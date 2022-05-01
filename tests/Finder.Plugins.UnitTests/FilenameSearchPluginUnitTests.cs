namespace Finder.Plugins.UnitTests;
using Finder.Plugins.Contracts;
using Finder.Plugins.FilenameSearch;
using System.Threading.Tasks;
using Xunit;

public class FilenameSearchPluginUnitTests
{
    [Fact]
    public async Task DoesNotThrowError()
    {
        var plugin = new FilenameSearchPlugin();
        var input = new PluginInput("search text");

        try
        {
            var output = await plugin.FindAsync(input);
        }
        catch
        {
            Assert.True(false, "This should not have thrown an error, fix it.");
        }
    }
}
