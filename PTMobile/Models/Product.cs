using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile.Models
{
    [Table("Product")]
    public class Product
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        string _name { get; set; }
        
            [Unique]
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return "";
                else
                    return _name;
            }
            set
            {
                _name = value;
            }
        }

        public double Price { get; set; }
        
        string _code { get; set; }
        [Unique]
        public string Code 
        { get
            {
                if (string.IsNullOrEmpty(_code))
                    return "";
                else
                    return _code;
            }
            set
            {
                _code = value;
            } 
        }

        public string Productimage { get; set; }

    }
}
