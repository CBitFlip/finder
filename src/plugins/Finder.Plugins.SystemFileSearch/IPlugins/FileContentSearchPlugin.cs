namespace Finder.Plugins.FilenameSearch;
using Finder.Plugins.Contracts;

public class FilenameSearchPlugin : IPlugin
{
    private FileIndexer? _indexer;

    public void Initialize()
    {
        _indexer = new FileIndexer();
    }

    public PluginOutput Find(PluginInput pluginInput)
    {
        return new PluginOutput(PluginResultStatus.Skipped, null);
    }

    public void Shutdown()
    {
        _indexer?.Dispose();
    }
}
