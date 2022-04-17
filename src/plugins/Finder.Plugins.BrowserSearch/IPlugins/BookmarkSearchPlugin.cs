namespace Finder.Plugins.BrowserSearch;
using Finder.Plugins.Contracts;
using System.Threading;
using System.Threading.Tasks;

public sealed class BookmarkSearchPlugin : IPlugin
{
    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    public Task<PluginOutput> FindAsync(PluginInput input, CancellationToken ctx = default)
    {
        throw new NotImplementedException();
    }

    public Task ShutdownAsync(CancellationToken ctx)
    {
        throw new NotImplementedException();
    }
}
