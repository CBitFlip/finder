# Finder console application

It finds stuff, because it is a finder.

## Tools

- Visual Studio 2022+ Community: https://visualstudio.microsoft.com/vs/community/
- Visual Studio Code: https://code.visualstudio.com/
- DotNet 6: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
- Golang, see: [readme link](/plugins/Finder.Plugins.SystemFileSearch.Dependencies/README.md)

### Visual Studio

When adding project, please use Visual Studio, not 'Visual Studio Code'.

### Visual Studio Code

To build:

```powershell
dotnet build
dotnet run --project src/Finder.ConsoleApp
```

To test:

```powershell
dotnet test
```

## Database Structure

Table: Words
id: int
value: nvarchar

Table: File
id: int
filename: nvarchar
path: nvarchar

Table: FileWordsMapping
wordId: int
fileId: int

## File Index Project Structure

Finder.Plugins.SystemFileSearch
|>Finder.Plugins.SystemFileSearch.Persistence

Finder.Plugins.SystemFileSearch.DataPrepper
|>Finder.Plugins.SystemFileSearch.Persistence
