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
            using (var db = new SimpleCalculatorContext())
            {
                foreach(HistoryEntry entry in db.HistoryEntries)
                {
                    db.HistoryEntries.Remove(entry);
                }
                db.SaveChanges();
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
