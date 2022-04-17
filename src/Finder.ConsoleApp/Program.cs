// See https://aka.ms/new-console-template for more information
using Finder;
using Finder.Plugins.BrowserSearch;
using Finder.Plugins.Contracts;
using Finder.Plugins.FilenameSearch;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services
            // Each basic plugin registration
            //.AddScoped<IPlugin, BookmarkSearchPlugin>()
            //.AddScoped<IPlugin, WebsiteSearchPlugin>()
            .AddScoped<IPlugin, FileContentSearchPlugin>()
            .AddScoped<IPlugin, FilenameSearchPlugin>()

            // As a hosted service / background service, this will trigger automatically
            //  on host run and it's cancellation token triggered when the host stops.
            .AddHostedService<ConsoleWorkerService>()
        )
    .Build();
Console.WriteLine("Welcome to the Finder, may it find for you well!");

AppDomain.CurrentDomain.ProcessExit += (_, _) =>
{
    Console.WriteLine("Received SIGTERM");
};

await host.RunAsync();
