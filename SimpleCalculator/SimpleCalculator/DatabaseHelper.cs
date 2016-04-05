using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    public static class DatabaseHelper
    {
        public static string dbfilename = "C:\\tmp\\simplecalculator.sqlite";

        public static void SetupDatabase()
        {
            if(!File.Exists(DatabaseHelper.dbfilename))
            {
                // Create db
                SQLiteConnection.CreateFile(DatabaseHelper.dbfilename);

                // Create history table
                using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + DatabaseHelper.dbfilename + ";Version=3;"))
                {
                    m_dbConnection.Open();
                    string createHistoryTableSql = "CREATE TABLE history (timestamp TEXT, command TEXT);";
                    using (SQLiteCommand createHistoryTable = new SQLiteCommand(createHistoryTableSql, m_dbConnection))
                    {
                        createHistoryTable.ExecuteNonQuery();
                    }
                }
            }
        }

        public static List<HistoryEntry> GetHistoryEntriesFromDb()
        {
            List<HistoryEntry> historyEntries = new List<HistoryEntry>();
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + DatabaseHelper.dbfilename + ";Version=3;"))
            {
                string sql = "select timestamp, command from history";
                using (SQLiteCommand sqlCommand = new SQLiteCommand(sql, m_dbConnection))
                {
                    m_dbConnection.Open();
                    using (SQLiteDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            HistoryEntry newEntry = new HistoryEntry()
                            {
                                Timestamp = reader["timestamp"].ToString(),
                                Command = reader["command"].ToString()
                            };
                            historyEntries.Add(newEntry);
                        }
                    }
                }
            }

            return historyEntries;
        }
        public static void ClearHistoryTable()
        {
            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + DatabaseHelper.dbfilename + ";Version=3;"))
            {
                m_dbConnection.Open();
                string sql = "delete from history";
                using (SQLiteCommand sqlCommand = new SQLiteCommand(sql, m_dbConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
