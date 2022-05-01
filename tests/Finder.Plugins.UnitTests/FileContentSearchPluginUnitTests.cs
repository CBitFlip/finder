namespace Finder.Plugins.UnitTests;
using Finder.Plugins.Contracts;
using Finder.Plugins.FilenameSearch;
using System.Threading.Tasks;
using Xunit;

public class FileContentSearchPluginUnitTests
{
    [Fact]
    public async Task DoesNotThrowError()
    {
        var plugin = new FileContentSearchPlugin();
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
