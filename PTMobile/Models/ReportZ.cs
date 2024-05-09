using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile.Models
{
    [Table("ReportZ")]
    public class ReportZ
    {
        [PrimaryKey]
        public string Id { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }

        public string DisplayDate { get; set; }
        public string TotalClass { get; set; }
        public string CashierId { get; set; }

    }
}
