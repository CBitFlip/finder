namespace Finder;
using Finder.Plugins.Contracts;
using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public sealed class ConsoleWorkerService : BackgroundService
{
    #region Setup

    private readonly List<IPlugin> _plugins;

    public ConsoleWorkerService(IEnumerable<IPlugin> plugins)
    {
        _plugins = plugins.ToList();
    }

    #endregion

    /// <summary>
    /// Triggered automatically from the <see cref="BackgroundService"/> and 
    /// Dependency-Injection <see cref="IHostedService"/> registration
    /// </summary>
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await InitializeAsync();

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                PrintHeader();
                var inputText = Console.ReadLine();

                var (outputs, exit) = await Process(inputText, stoppingToken);
                if (exit) 
                    break;

                if (outputs is not null)
                    PrintResults(inputText, outputs.ToList());
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception) { }

        await ShutdownAsync();
    }

    private async Task<(ConcurrentQueue<PluginOutput>?, bool)> Process(string? inputText, CancellationToken stoppingToken)
    {
        if (string.IsNullOrWhiteSpace(inputText))
            return (null, false);
        if (inputText == "quit" || inputText == "exit")
            return (null, true);

        var input = new PluginInput(inputText);
        var outputs = new ConcurrentQueue<PluginOutput>();
        foreach (var plugin in _plugins)
        {
            var result = await plugin.FindAsync(input, stoppingToken);
            if (result.Status == PluginResultStatus.Skipped)
                continue;

            outputs.Enqueue(result);
        }

        return (outputs, false);
    }

    #region Setup/Tear down Actions

    private async Task InitializeAsync()
    {
        var po = new ParallelOptions() { MaxDegreeOfParallelism = 2 };
        await Parallel.ForEachAsync(_plugins, po, async (p, ctx) => await p.InitializeAsync());

        Console.CancelKeyPress += async (_, ea) =>
        {
            // Tell .NET to not terminate the process
            ea.Cancel = true;

            Console.WriteLine("Received SIGINT (Ctrl+C)");
            await ShutdownAsync();
        };
    }

    private async Task ShutdownAsync()
    {
        var po = new ParallelOptions() { MaxDegreeOfParallelism = 2 };
        await Parallel.ForEachAsync(_plugins, po, async (p, ctx) => await p.ShutdownAsync(ctx));
    }

    #endregion

    #region Console Printer Helpers 

    private static void PrintHeader()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Welcome to the Finder, may it find for you well!");
        Console.Write("Search:                                             ");
        Console.SetCursorPosition(8, 1);
    }

    private static void PrintResults(string? inputText, List<PluginOutput> outputs)
    {
        Console.WriteLine();
        Console.WriteLine($"Result of '{inputText}':");

        for (int i = 0; i < outputs.Count; i++)
        {
            Console.WriteLine($"#{i + 1}: {outputs[i].Status} - etc.");
        }
    }

    #endregion
}
