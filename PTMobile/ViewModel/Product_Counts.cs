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
    public class Product_Counts : ObservableObject
    {
        public ObservableCollection<Product_Count> ProductCountsList { get; set; } = new();
        public ObservableCollection<Product_Count> ProductCountsListAll { get; set; } = new();

        public Product_Counts()
        {
            
            foreach (var item in App.CashierData.GetAllProductCounts())
            {
                ProductCountsListAll.Add(item);
            }
            if (string.IsNullOrEmpty(MainPage.WareHouse))
            {
                if (MainPage.Todos)
                {
                    MainPage.InProgress = true;
                    MainPage.Processed = true;
                    MainPage.Canceled = true;

                }
                else
                {
                    if (MainPage.InProgress)
                    {
                        foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 0))
                        {
                            ProductCountsList.Add(item);
                        }
                    }
                    if (MainPage.Processed)
                    {
                        foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 1))
                        {
                            ProductCountsList.Add(item);
                        }
                    }
                    if (MainPage.Canceled)
                    {
                        foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 2))
                        {
                            ProductCountsList.Add(item);
                        }
                    }

                }
            }
            else
            {
                if (MainPage.Todos)
                {
                    MainPage.InProgress = true;
                    MainPage.Processed = true;
                    MainPage.Canceled = true;

                }
                else
                {
                    if (MainPage.InProgress)
                    {
                        foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 0 && r.Warehouse == MainPage.WareHouse))
                        {
                            ProductCountsList.Add(item);
                        }
                    }
                    if (MainPage.Processed)
                    {
                        foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 1 && r.Warehouse == MainPage.WareHouse))
                        {
                            ProductCountsList.Add(item);
                        }
                    }
                    if (MainPage.Canceled)
                    {
                        foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 2 && r.Warehouse == MainPage.WareHouse))
                        {
                            ProductCountsList.Add(item);
                        }
                    }

                }
            }
           

        }
        public Product_Counts(DateTime minDate,DateTime maxDate,bool begin)
        {
            if (begin)
            {
                foreach (var item in App.CashierData.GetAllProductCounts())
                {
                    ProductCountsListAll.Add(item);
                }
                if (string.IsNullOrEmpty(MainPage.WareHouse))
                {
                    if (MainPage.Todos)
                    {
                        MainPage.InProgress = true;
                        MainPage.Processed = true;
                        MainPage.Canceled = true;

                    }
                    else
                    {
                        if (MainPage.InProgress)
                        {
                            foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 0 && r.StartDate>minDate && r.StartDate<maxDate))
                            {
                                ProductCountsList.Add(item);
                            }
                        }
                        if (MainPage.Processed)
                        {
                            foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 1 && r.StartDate > minDate && r.StartDate < maxDate))
                            {
                                ProductCountsList.Add(item);
                            }
                        }
                        if (MainPage.Canceled)
                        {
                            foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 2 && r.StartDate > minDate && r.StartDate < maxDate))
                            {
                                ProductCountsList.Add(item);
                            }
                        }

                    }
                }
                else
                {
                    if (MainPage.Todos)
                    {
                        MainPage.InProgress = true;
                        MainPage.Processed = true;
                        MainPage.Canceled = true;

                    }
                    else
                    {
                        if (MainPage.InProgress)
                        {
                            foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 0 && r.Warehouse == MainPage.WareHouse && r.StartDate > minDate && r.StartDate < maxDate))
                            {
                                ProductCountsList.Add(item);
                            }
                        }
                        if (MainPage.Processed)
                        {
                            foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 1 && r.Warehouse == MainPage.WareHouse && r.StartDate > minDate && r.StartDate < maxDate))
                            {
                                ProductCountsList.Add(item);
                            }
                        }
                        if (MainPage.Canceled)
                        {
                            foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 2 && r.Warehouse == MainPage.WareHouse && r.StartDate > minDate && r.StartDate < maxDate))
                            {
                                ProductCountsList.Add(item);
                            }
                        }

                    }
                }
            }
            else
            {
                foreach (var item in App.CashierData.GetAllProductCounts())
                {
                    ProductCountsListAll.Add(item);
                }
                if (string.IsNullOrEmpty(MainPage.WareHouse))
                {
                    if (MainPage.Todos)
                    {
                        MainPage.InProgress = true;
                        MainPage.Processed = true;
                        MainPage.Canceled = true;

                    }
                    else
                    {
                        if (MainPage.InProgress)
                        {
                            foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 0 && r.EndDate > minDate && r.EndDate < maxDate))
                            {
                                ProductCountsList.Add(item);
                            }
                        }
                        if (MainPage.Processed)
                        {
                            foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 1 && r.EndDate > minDate && r.EndDate < maxDate))
                            {
                                ProductCountsList.Add(item);
                            }
                        }
                        if (MainPage.Canceled)
                        {
                            foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 2 && r.EndDate > minDate && r.EndDate < maxDate))
                            {
                                ProductCountsList.Add(item);
                            }
                        }

                    }
                }
                else
                {
                    if (MainPage.Todos)
                    {
                        MainPage.InProgress = true;
                        MainPage.Processed = true;
                        MainPage.Canceled = true;

                    }
                    else
                    {
                        if (MainPage.InProgress)
                        {
                            foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 0 && r.Warehouse == MainPage.WareHouse && r.EndDate > minDate && r.EndDate < maxDate))
                            {
                                ProductCountsList.Add(item);
                            }
                        }
                        if (MainPage.Processed)
                        {
                            foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 1 && r.Warehouse == MainPage.WareHouse && r.EndDate > minDate && r.EndDate < maxDate))
                            {
                                ProductCountsList.Add(item);
                            }
                        }
                        if (MainPage.Canceled)
                        {
                            foreach (var item in App.CashierData.GetAllProductCounts().Where(r => r.Status == 2 && r.Warehouse == MainPage.WareHouse && r.EndDate > minDate && r.EndDate < maxDate))
                            {
                                ProductCountsList.Add(item);
                            }
                        }

                    }
                }
            }
        }
    }
}
