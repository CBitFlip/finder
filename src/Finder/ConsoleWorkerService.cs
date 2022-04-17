namespace Finder
{
    using Finder.Plugins.Contracts;
    using Microsoft.Extensions.Hosting;
using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class ConsoleWorkerService : BackgroundService
    {
        private readonly List<IPlugin> _plugins;

        public ConsoleWorkerService(IEnumerable<IPlugin> plugins)
        {
            _plugins = plugins.ToList();
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await InitializeAsync();

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var exit = await Process(stoppingToken);
                    if (exit) break;
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception) { }

            await ShutdownAsync();
        }

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

        private async Task<bool> Process(CancellationToken stoppingToken)
        {
            Console.WriteLine();
            Console.Write("Search: ");
            var inputText = Console.ReadLine();

            if (inputText == "quit" || inputText == "exit") 
                return true;
            if (string.IsNullOrWhiteSpace(inputText)) 
                return false;

            var input = new PluginInput(inputText);
            var outputs = new ConcurrentQueue<PluginOutput>();
            foreach (var plugin in _plugins)
            {
                var result = await plugin.FindAsync(input, stoppingToken);
                if (result.Status == PluginResultStatus.Skipped)
                    continue;

                outputs.Enqueue(result);
            }

            foreach (var output in outputs)
            {
                Console.WriteLine($"{output.Status} - etc.");
            }
            return false;
        }

        private async Task ShutdownAsync()
        {
            var po = new ParallelOptions() { MaxDegreeOfParallelism = 2 };
            await Parallel.ForEachAsync(_plugins, po, async (p, ctx) => await p.ShutdownAsync(ctx));
        }
    }
}