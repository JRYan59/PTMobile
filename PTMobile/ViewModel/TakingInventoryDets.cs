using CommunityToolkit.Mvvm.ComponentModel;
using PTMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile.ViewModel
{
    public class TakingInventoryDets : ObservableObject
    {
        public ObservableCollection<Product_Count_Det_Display> ProductCountDetsList { get; set; } = new();

        public TakingInventoryDets(CashierDatabase cashierData)
        {
            foreach (var item in cashierData.GetProduct_Count_Dets(MainPage.Product_Count.Id))
            {

                ProductCountDetsList.Add(item);
            }
            
        }
    }
}
