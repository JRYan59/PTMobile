using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile.Models
{
    [Table("ProductImage")]
    public class ProductImage
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string Img { get; set; }

        [Unique]
        public string Name { get; set; }

        public string ProductCode { get; set; }

        public string Path { get; set; }



    }
}
