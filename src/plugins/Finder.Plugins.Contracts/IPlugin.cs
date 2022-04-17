namespace Finder.Plugins.Contracts;

public interface IPlugin
{
    public void Initialize();
    public PluginOutput Find(PluginInput pluginInput);
    public void Shutdown();
}
