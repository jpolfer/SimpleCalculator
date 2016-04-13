using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SimpleCalculator
{
    public class SimpleCalculatorContext : DbContext
    {
        public SimpleCalculatorContext() : base("HistoryConnection") { }

        public DbSet<HistoryEntry> HistoryEntries { get; set; }
    }
}