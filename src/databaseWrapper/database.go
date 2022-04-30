package main

import (
	"database/sql"
	"log"

	_ "github.com/mattn/go-sqlite3"
)

func _establishDatabase(pathToDatabase string) *sql.DB {

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

	for rows.Next() {
		var path string
		rows.Scan(&path)
		log.Println(path)
	}
}

func addToFileTable(pathToDatabase string, pathToFile string) {
	// open the database. Currently assumes the database doesn't already exist
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

func closeDatabase(database *sql.DB) {
	database.Close()
}
