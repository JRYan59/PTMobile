using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile.Models
{
    [Table("Cashier")]
    public class Cashier
    {
        [PrimaryKey]
        public string Id { get; set; }

        [MaxLength(50),Unique]
        public string Name { get; set; }
        //public string Code { get; set; }
    }
}
