package main

import (
	"database/sql"
	"fmt"
	"log"

	_ "github.com/mattn/go-sqlite3"
)

type file struct {
	name      string
	path      string
	extension string
}

func establishDatabase(pathToDatabase string) *sql.DB {

	// open the database. Currently assumes the database doesn't already exist
	db, err := sql.Open("sqlite3", pathToDatabase)

	if err != nil {
		log.Println("Unable to open db" + pathToDatabase + ".")
	}

	//create the database
	db.Exec("create table file (id integer, name text, path text, type text)")

	return db
} /* establishDatabase */

func getFromFileTable(pathToDatabase string) {
	// open the database. Currently assumes the database doesn't already exist
	db, err := sql.Open("sqlite3", pathToDatabase)
	defer db.Close()

	if err != nil {
		log.Println("Unable to open db" + pathToDatabase + ".")
		return
	}
	rows, err := db.Query("select path from file order by name")
	defer rows.Close()

	for rows.Next() {
		var path string
		rows.Scan(&path)
		log.Println(path)
	}
}

func addToFileTable(pathToDatabase string, pathToFile string) {
	db, err := sql.Open("sqlite3", pathToDatabase)
	defer db.Close()

	if err != nil {
		log.Println("Unable to open db" + pathToDatabase + ".")
		return
	}

	statement, err := db.Prepare("insert into file(id, name, path, type) values(?, ?, ?, ?)")
	if err != nil {
		log.Println("Unable to prepare insert into file.")
		return
	}
	defer statement.Close()

	statement.Exec(1, pathToFile, pathToFile, "pdf")
}

func findFile(pathToDatabase string, fileQuery string) []file {
	var count int
	var foundFiles []file

	db, err := sql.Open("sqlite3", pathToDatabase)
	defer db.Close()

	if err != nil {
		log.Println("Unable to open db" + pathToDatabase + ".")
		return nil
	}

	rows, err := db.Query("select name from file where name like '%" + fileQuery + "%' order by name")
	if err != nil {
		log.Println("Unable to run query")
		log.Println(err)
		return nil
	}
	defer rows.Close()

	for rows.Next() {
		count += 1
		var name string
		rows.Scan(&name)
		log.Println(name)
		foundFiles = append(foundFiles, file{name, "", ""})
	}

	if count == 0 {
		fmt.Println("No files found for query " + fileQuery)
	}

	return foundFiles
} //findFile

func closeDatabase(database *sql.DB) {
	database.Close()
}
