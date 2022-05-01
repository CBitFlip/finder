# finderDB

db layer for finder

## configure go environment

1. install go https://go.dev/doc/install
1. Make sure 64bit gcc is in your path
1. mingw
   - SourceForge
     1. https://sourceforge.net/projects/mingw-w64/files/mingw-w64/ (I picked x86_64-win32-seh)
     1. copy git repo

### Configuring with Chocolatey

```powershell
# download choco: https://chocolatey.org/install
Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))

# Install Golang and the c-compiler dependencies
choco install golang -y
choco install mingw -y

# Install gRPC
choco install protoc -y
go get google.golang.org/grpc
```

## Visual Studio / VS Code

Open up databaseWrapper.code-workspace, which is a Visual Studio Code project workspace. Once ready, and setup, call the build and such steps below.

## build steps

- go build -buildmode=c-shared -buildvcs=false -o ../finderDB.dll

## installation steps
