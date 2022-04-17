namespace Finder.Plugins.UnitTests;
using Finder.Plugins.Contracts;
using Finder.Plugins.FilenameSearch;
using Xunit;

public class FilenameSearchPluginUnitTests
{
    [Fact]
    public void DoesNotThrowError()
    {
        var plugin = new FilenameSearchPlugin();
        var input = new PluginInput();

        try
        {
            var output = plugin.Find(input);
        }
        catch
        {
            Assert.True(false, "This should not have thrown an error, fix it.");
        }
    }
}
