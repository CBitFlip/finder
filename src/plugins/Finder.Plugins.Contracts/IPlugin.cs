namespace Finder.Plugins.Contracts;

public interface IPlugin
{
    public PluginOutput Find(PluginInput pluginInput);
}
