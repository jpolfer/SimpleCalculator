using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    public static class DatabaseHelper
    {
        public static void SetupDatabase()
        {
            // no longer used
        }

        public static List<HistoryEntry> GetHistoryEntriesFromDb()
        {
            using (var db = new SimpleCalculatorContext())
            {
                List<HistoryEntry> historyEntries = db.HistoryEntries
                                                      .ToArray<HistoryEntry>()
                                                      .OrderBy(x => Convert.ToDateTime(x.Timestamp))
                                                      .ToList<HistoryEntry>();
                return historyEntries;
            }
            return new List<HistoryEntry>();
        }

        public static void ClearHistoryTable()
        {
            // Call stored proc
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["HistoryConnection"].ConnectionString))
            using (var command = new SqlCommand("master.dbo.spDeleteAllHistoryEntries", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static bool StoreEvaluationToHistoryTable(string command, string result)
        {
            using (var db = new SimpleCalculatorContext())
            {
                try
                {
                    HistoryEntry newEntry = new HistoryEntry()
                    { Command = command + " = " + result,
                      Timestamp = DateTime.Now.ToString()
                    };
                    db.HistoryEntries.Add(newEntry);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
