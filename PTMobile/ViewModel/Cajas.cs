using PTMobile.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile.ViewModel
{
    public partial class Cajas
    {
        public  List<Cashier> CashierList { get; set; } = new List<Cashier>();
        List<Cashier> cajas = new List<Cashier>();




        public Cajas()
        {
            GetDataCashiers();

            foreach (var item in cajas)
            {
                Cashier cashier = new Cashier()
                {
                    Id = item.Id,
                    Name = item.Name

                };
                CashierList.Add(cashier);
            }
            
            
        }

        private void GetDataCashiers()
        {
            cajas = App.CashierData.GetAllCashiers();

            
        }





    }
}
