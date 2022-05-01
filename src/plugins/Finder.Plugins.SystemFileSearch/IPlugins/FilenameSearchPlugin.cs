namespace Finder.Plugins.FilenameSearch;
using Finder.Plugins.Contracts;

public sealed class FilenameSearchPlugin : IPlugin
{
    private FilenameIndexer? _indexer;

    public Task InitializeAsync()
    {
        _indexer = new FilenameIndexer();
        return Task.CompletedTask;
    }

    public Task<PluginOutput> FindAsync(PluginInput input, CancellationToken ctx = default)
    {
        var output = new PluginOutput(PluginResultStatus.Skipped, null);

        return Task.FromResult(output);
    }

    public Task ShutdownAsync(CancellationToken ctx)
    {
        _indexer?.Dispose();
        return Task.CompletedTask;
    }
}
