namespace Finder.Plugins.Contracts;

public interface IPlugin
{
    public Task InitializeAsync();
    public Task<PluginOutput> FindAsync(PluginInput input, CancellationToken ctx);
    public Task ShutdownAsync(CancellationToken ctx);
}
