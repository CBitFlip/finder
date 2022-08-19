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

Table: File
id: int
filename: nvarchar
last-access: int (ticks) ? idea is we can have last accessed sooner in the index, as the user probably is revisiting files
path: nvarchar

Table: Words
id: int
value: nvarchar

Table: FileWordsMapping
wordId: int
fileId: int
