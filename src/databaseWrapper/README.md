# finderDB
 db layer for finder

## configure go environment
 * install go https://go.dev/doc/install
 * Make sure 64bit gcc is in your path 
 * https://sourceforge.net/projects/mingw-w64/files/mingw-w64/ (I picked x86_64-win32-seh)
 * copy git repo

## build steps
 * go build -buildmode=c-shared -buildvcs=false

## command-line options
* `-create` creates the database if it doesn't already exist. Establishes the necessary tables. Required for each call
* `-add`    adds a file to the database
* `-find`   finds files in the database that match the string provided. Runs query ... like '%search%'

## example
* finderDB.exe -create path/to/database.db
* finderDB.exe -create path/to/database.db -add path/to/fileWithLongName.txt
* finderDB.exe -create path/to/database/db -find fileWith

## exported functions
* `createDatabase(pathAndNameOfDatabase)` equivalent to the `-create` command-line option
* `addFile(pathAndNameOfFile)` equivalent to the `-add` command-line option
* `find(searchString)` equivalent to the `-find` command-line option
* `getAllFiles()` prints all files currently in the finderDB database

## installation steps

