using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SimpleCalculator
{
    public class HistoryEntry
    {
        [Key]
        public string Timestamp { get; set; }

        public string Command { get; set; }
    }
}
