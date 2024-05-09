using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile.Models
{
    [Table("Product_Count")]
    public class Product_Count
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 0: En proceso, 1:Procesado, 2:Anulado
        /// </summary>
        public int Status { get; set; }
        public string Warehouse { get; set; }
        public string Descr { get; set; }
        public int UserId { get; set; }

        public bool Ended
        {
            get
            {
                return Status != 0;
            }
        }
        public string DisplayStatus
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return "En proceso";
                    case 1:
                        return "Procesado";
                    case 2:
                        return "Anulado";
                    default:
                        return "Sin Status";
                }
            }
        }
    }
}
