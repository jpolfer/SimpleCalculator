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

    }
}
