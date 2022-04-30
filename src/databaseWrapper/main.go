package main

import (
	"fmt"

	_ "github.com/mattn/go-sqlite3"
)

var database string

func main() {
	fmt.Println("Hello Worlds")

	db := _establishDatabase("C:/Users/ststa/Documents/GitHub/finderDB/test.db")
	defer db.Close()

	addFile("C:/Users/ststa/Desktop/test.pdf")
	getFiles()

}

//export establishDatabase
func establishDatabase(databaseName string) {
	database = databaseName
	db := _establishDatabase(database)
	defer db.Close()
}

//export addFile
func addFile(pathToFile string) {
	addToFileTable(database, pathToFile)
}

//export getFiles
func getFiles() {
	getFromFileTable(database)
}
