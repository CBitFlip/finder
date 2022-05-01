namespace Finder.Plugins.FilenameSearch;
using Finder.Plugins.Contracts;

public sealed class FileContentSearchPlugin : IPlugin
{
    private FileContentIndexer? _indexer;

    public Task InitializeAsync()
    {
        _indexer = new FileContentIndexer();
        return Task.CompletedTask;
    }

    /// <remark>
    /// Thoughts to utilize:
    /// * Window indexes
    /// * On-demand search
    /// * Database index lookup search
    /// </remark>
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
