using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile.Models
{
    [Table("Product_Count_Det")]
    public class Product_Count_Det
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int Id_Pc { get; set; }
        public string ProductCode { get; set; }
        public int Qty { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}
