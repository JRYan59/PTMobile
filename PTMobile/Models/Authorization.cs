using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile.Models
{
    [Table("Authorization")]
    public class Authorization
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Code { get; set; }
        public string DetailDescription { get; set; }
        public string Description { get; set; }
        public string CapturePath { get; set; }
        /// <summary>
        /// 0: Por Validar, 1:Autorizado, 2:Rechazado
        /// </summary>
        public int Status { get; set; }
        public DateTime CheckDate { get; set; }
        [Unique]
        public string IdAuth { get; set; }

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
                        return "Por Validar";
                    case 1:
                        return "Aprobado";
                    case 2:
                        return "Rechazado";
                    default:
                        return "Sin Status";
                }
            }
        }
    }
}
