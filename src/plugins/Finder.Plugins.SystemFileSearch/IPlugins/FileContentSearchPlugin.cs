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
        throw new NotImplementedException();
    }

    public void Shutdown()
    {
        _indexer?.Dispose();
    }
}
