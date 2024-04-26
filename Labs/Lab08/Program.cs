using System;
using System.Collections.Generic;
using System.IO;  
using Microsoft.Data.Sqlite;
using System.Linq;

class Program  
{  
    static void Main()  
    {  
       // Task_1("territories.csv", ',');
        (List<List<string>> data, List<string> columnNames) = ReadCsv("territories.csv", ',');
       // Task_2(data,columnNames);
        Dictionary<string, (string type, bool nullable)> columnsInfo= AnalyzeColumns(data, columnNames);
       // SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());
        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        connectionStringBuilder.DataSource = "table.db";

        using var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
        connection.Open();
        CreateTable(columnsInfo, "test", connection);
        InsertData(data, columnNames, "test", connection);
        SelectData("test", connection);
        connection.Close(); 




        
    }  

    public static (List<List<string>>, List<string>) ReadCsv(string file, char separator)
    {
        List<List<string>> data = new List<List<string>>();
        List<string> headers = new List<string>();

        using (var reader = new StreamReader(file))
        {
            bool isHeader = true;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(separator);

                if (isHeader)
                {
                    headers.AddRange(values);
                    isHeader = false;
                } 
                else
                {
                    data.Add(new List<string>(values));
                }
            }
        }

        return (data, headers);
    }

    public static  Dictionary<string, (string type, bool nullable)> AnalyzeColumns(List<List<string>> data, List<string> headers){
        Dictionary<string, (string type, bool nullable)> columnTypes = new Dictionary<string, (string type, bool nullable)>();

        foreach (var columnName in headers)
        {
            bool allInt = true;
            bool allDouble = true;
            bool allNull = true;
        
            foreach(var row in data){
                string value = row[headers.IndexOf(columnName)] as string;
                if(string.IsNullOrEmpty(value)){
                    allNull = false;
                }
                else{
                    if(!int.TryParse(value, out _)){
                        allInt = false;
                    }
                    if(!double.TryParse(value, out _)){
                        allDouble = false;
                    }
                }
            }
            if(allInt){
                columnTypes[columnName] = ("INTEGER", allNull);
            }
            else if(allDouble){
                columnTypes[columnName] = ("REAL",allNull);
            }
            else{
                columnTypes[columnName] = ("TEXT",allNull);
            }
        }
        return columnTypes;
    }

    public static void CreateTable(Dictionary<string, (string type, bool nullable)> columnTypes, string tableName, SqliteConnection connection){
        try {
                SqliteCommand delTableCmd = connection.CreateCommand();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS "+tableName;
                delTableCmd.ExecuteNonQuery();

                string query = "CREATE TABLE " + tableName + " (";
                foreach(var column in columnTypes){
                    query += column.Key + " " + column.Value.type;
                    if (column.Value.nullable){
                        query += " NULLABLE" ;
                    }
                    else{
                        query += " NOT NULLABLE";
                } 
                query += ",";
                }
                query = query.TrimEnd(',') + ");";
                SqliteCommand createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = query;
               
                createTableCmd.ExecuteNonQuery();
                Console.WriteLine("Table created!");
                } catch (Exception e){ 
                Console.WriteLine(e.Message);
                }
    }

    public static void InsertData(List<List<string>> data, List<string> headers, string tableName,  SqliteConnection connection){
        SqliteCommand insertCmd = connection.CreateCommand();
        string query = "INSERT INTO "+ tableName + " (";
        foreach (var header in headers){
            query += header + ",";
        }
        query = query.TrimEnd(',') + ") VALUES ";

        int intValue;
        double doubleValue;

        foreach(var row in data){
            query += "(";
            foreach(var column in row){
                if(column == "")
                {
                    query += "@column,";
                    continue;
                }
                else if(!int.TryParse(column, out intValue) & !double.TryParse(column, out doubleValue))
                {
                    query += "\"" + column + "\",";
                    continue;
                }
                query += column + ",";
            }
            query = query.TrimEnd(',') + "),";
        }
        query = query.TrimEnd(',');
        insertCmd.CommandText = query;
        insertCmd.ExecuteNonQuery();
        Console.WriteLine("Data inserted!");
    }

    public static void SelectData(string tableName, SqliteConnection connection){
        SqliteCommand selectCmd = connection.CreateCommand();
        selectCmd.CommandText = "SELECT * FROM "+tableName+" where regionid > 1;";
        selectCmd.ExecuteNonQuery();
        using (SqliteDataReader reader = selectCmd.ExecuteReader())
            {
                bool firstRow = true;
                while (reader.Read())
                {
                    if (firstRow)
                    {
                        for (int a = 0; a < reader.FieldCount; a++)
                        {
                            Console.Write(reader.GetName(a));
                            Console.Write(",");
                        }
                        firstRow = false;
                        Console.WriteLine("");
                    }
                    for (int a = 0; a < reader.FieldCount; a++)
                    {
                        String?val = null;
                        try {
                            val = reader.GetString(a);
                        } catch {}
                        Console.Write(val != null ? val : "NULL");
                        Console.Write(",");
                    }
                    Console.WriteLine("");
                }
                reader.Close();
        }
    }

    public static void Task_1(string file, char separator){
        (List<List<string>> data, List<string> columnNames) = ReadCsv(file, separator);
        Console.WriteLine("Column names:");
        foreach (string columnName in columnNames)
        {
            Console.WriteLine(columnName);
        }

        Console.WriteLine("\nData:");
        foreach (List<string> row in data)
        {
            foreach (string value in row)
            {
                Console.Write(value + "\t");
            }
            Console.WriteLine();
        }
    }

    public static void Task_2(List<List<string>> data, List<string> headers){
        Dictionary<string, (string type, bool nullable)> columnsInfo= AnalyzeColumns(data, headers);
        Console.WriteLine("Column types:");

        foreach (var column in columnsInfo)
        {
            string columnType = column.Value.type;
            bool nullable = column.Value.nullable;

            string nullableString = nullable ? "nullable" : "not nullable";

            Console.WriteLine($"{column.Key}: {columnType} ({nullableString})");
    }
}
}