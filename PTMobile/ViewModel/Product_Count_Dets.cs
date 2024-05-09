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
    public class Product_Count_Dets : ObservableObject
    {
        public ObservableCollection<Product_Count_Det> ProductCountDetsList { get; set; } = new();

        public Product_Count_Dets(CashierDatabase cashierData)
        {
            foreach (var item in cashierData.GetProduct_Count_Dets(MainPage.Product_Count.Id, MainPage.CurrentProductCode))
            {

                ProductCountDetsList.Add(item);
            }
            
        }
    }
}
