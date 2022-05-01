package main

import (
	"fmt"
	"os"

	_ "github.com/mattn/go-sqlite3"
)

var database string

func main() {
	arguments := os.Args[1:]

	fmt.Println("Welcome to Finder's database")
	fmt.Printf("Arguments: %v\n", arguments)

	for index, argument := range arguments {

		switch argument {
		//print help info
		case "-h":
			fmt.Println("finderDB is the database for the file/folder related plugins for Finder.")
		//create the finderDB database
		case "-create":
			if len(arguments) < index+2 {
				fmt.Println("Unable to create the database: no path or database name provided.")
				return
			}

			if _, err := os.Lstat(arguments[index+1]); err != nil {
				fmt.Println("Creating a database at " + arguments[index+1])
				createDatabase(arguments[index+1])
			}

			database = arguments[index+1]
		//add a file/folder to the database
		case "-add":
			if len(arguments) < index+2 {
				fmt.Println("Unable to add the following file/folder: no path or file name provided.")
			} else {
				info, err := os.Lstat(arguments[index+1])
				if err != nil {
					fmt.Println(err)
					return
				}
				fmt.Println("Adding  " + info.Name())
				addFile(arguments[index+1])
			}

		case "-find":
			if len(arguments) < index+2 {
				return
			}

			find(arguments[index+1])
		} //switch argument
	} //for argument in arguments
}

//export createDatabase
func createDatabase(databaseName string) {
	database = databaseName
	db := establishDatabase(database)
	defer db.Close()
}

//export addFile
func addFile(pathToFile string) {
	addToFileTable(database, pathToFile)
}

//export addDirectory
func addDirectory(pathToDirectory string) {

}

//export find
func find(query string) string {
	findFile(database, query)

	return ""
}

//export getFiles
func getFiles() {
	getFromFileTable(database)
}
