using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile.Models
{
    [Table("ProductImageLocal")]
    public class ProductImageLocal
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime Date { get; set; }
        /// <summary>
        /// 1. Modificado 2.Borrado
        /// </summary>
        public int Status { get; set; }
    }   
}
