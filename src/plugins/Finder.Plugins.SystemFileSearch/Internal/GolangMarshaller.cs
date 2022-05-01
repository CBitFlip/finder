namespace Finder.Plugins.SystemFileSearch.Internal;
using System.Runtime.InteropServices;

internal static class GolangMarshaller
{
    /// <summary>
    /// Starts the indexing processes, which place results into the common SQLite storage.
    /// </summary>
    [DllImport("finderDB", EntryPoint = "main")]
    extern static void StartIndexer();

    /// <summary>
    /// Stops the indexing processes.
    /// </summary>
    [DllImport("finderDB", EntryPoint = "stop")]
    extern static void StopIndexer();

    /// <summary>
    /// Fetches file names that match the pattern.
    /// </summary>
    /// <param name="regexPattern">A regex pattern to match the filename against</param>
    /// <returns>List of matching, fully-qualified filenames.</returns>
    [DllImport("finderDB", EntryPoint = "getFiles")]
    extern static string[] GetFiles(string regexPattern);
}
