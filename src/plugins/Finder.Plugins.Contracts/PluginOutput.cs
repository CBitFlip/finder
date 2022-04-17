namespace Finder.Plugins.Contracts;

public record PluginOutput(PluginResultStatus Status, Exception? Exception);

public enum PluginResultStatus
{
    Unknown,
    Skipped,
    Timeout,
    Errored,
    Executed
}
