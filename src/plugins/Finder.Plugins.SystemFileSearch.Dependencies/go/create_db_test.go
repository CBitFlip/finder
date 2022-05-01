package main

import (
	"fmt"
	"os"
	"strings"
	"testing"

	"github.com/stretchr/testify/assert"
)

func TestHelloWorld(t *testing.T) {
	fmt.Println("Hello World")
}

/*
 * Creates a finder database in SQLite3:
 *   1) creates the db file
 *   2) creates the database
 *   3) creates the tables, fields
 *   4) queries the structure of the database for correctness
 */
func TestCreateDB(t *testing.T) {
	expectedTables := "create table file (id integer, name text, path text, type text)"
	db := _establishDatabase("test.db")
	assert.True(t, db != nil)
	defer func() {
		db.Close()
		os.Remove("test.db")
	}()

	// add below logic to test
	rows, err := db.Query("select * from sqlite_schema where type='table' order by name")
	assert.True(t, rows != nil && err == nil, err)

	for rows.Next() {
		var sql string
		rows.Scan(&sql)
		assert.Equal(t, expectedTables, strings.ToLower(sql))
	}
}
