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
    public class Products : ObservableObject
    {
        public ObservableCollection<Product> ProductList { get; set; } = new ObservableCollection<Product>();
        
        private string Product = string.Empty;

        private int Order = 0;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// 0:Alfabetico,1:Codigo
        /// <param name="orderType"></param>
        public Products(string product,int orderType)
        {
            Product = product;
            Order = orderType;
            FilterProducts();

        }

        private void FilterProducts()
        {
            if(Product != string.Empty)
            {
                foreach (var item in App.CashierData.GetProducts(Product))
                {
                    ProductList.Add(item);
                }
                if(Order == 0)
                {
                    List<Product> newSort = ProductList.OrderByDescending(x => x.Name).ToList();
                    ProductList.Clear();
                    foreach (var item in newSort)
                    {
                        ProductList.Add(item);
                    }
                }
                else
                {
                    List<Product> newSort = ProductList.OrderByDescending(x => x.Code).ToList();
                    ProductList.Clear();
                    foreach (var item in newSort)
                    {
                        ProductList.Add(item);
                    }
                }
                

                
            }                
            else
            {
                foreach (var item in App.CashierData.GetProducts())
                {
                    ProductList.Add(item);
                }
            }
            
        }
    }
}
