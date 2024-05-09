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
    public class Authorizations : ObservableObject
    {
        public ObservableCollection<Authorization> AuthorizationsList{ get; set; } = new();
        public ObservableCollection<Authorization> AuthorizationsListAll { get; set; } = new();

        public Authorizations()
        {
            foreach (var item in App.CashierData.GetAllAuthorizations())
            {
                AuthorizationsListAll.Add(item);
            }
            if (MainPage.TodosAuth)
            {
                MainPage.ToValidate = true;
                MainPage.Authorized = true;
                MainPage.Reject = true;
            }
            else
            {
                if (MainPage.ToValidate)
                {
                    foreach (var item in App.CashierData.GetAllAuthorizations().Where(r=>r.Status==0))
                    {
                        AuthorizationsList.Add(item);
                    }
                }
                if (MainPage.Authorized)
                {
                    foreach (var item in App.CashierData.GetAllAuthorizations().Where(r => r.Status == 1))
                    {
                        AuthorizationsList.Add(item);
                    }
                }
                if (MainPage.Reject)
                {
                    foreach (var item in App.CashierData.GetAllAuthorizations().Where(r => r.Status == 2))
                    {
                        AuthorizationsList.Add(item);
                    }
                }

            }
        }

        public Authorizations(DateTime minDate,DateTime maxDate,bool ischecked)
        {
            if (ischecked == false)
            {
                foreach (var item in App.CashierData.GetAllAuthorizations())
                {
                    AuthorizationsListAll.Add(item);
                }
                if (MainPage.TodosAuth)
                {
                    MainPage.ToValidate = true;
                    MainPage.Authorized = true;
                    MainPage.Reject = true;
                }
                else
                {
                    if (MainPage.ToValidate)
                    {
                        foreach (var item in App.CashierData.GetAllAuthorizations().Where(r => r.Status == 0 && r.Date > minDate && r.Date < maxDate))
                        {
                            AuthorizationsList.Add(item);
                        }
                    }
                    if (MainPage.Authorized)
                    {
                        foreach (var item in App.CashierData.GetAllAuthorizations().Where(r => r.Status == 1 && r.Date > minDate && r.Date < maxDate))
                        {
                            AuthorizationsList.Add(item);
                        }
                    }
                    if (MainPage.Reject)
                    {
                        foreach (var item in App.CashierData.GetAllAuthorizations().Where(r => r.Status == 2 && r.Date > minDate && r.Date < maxDate))
                        {
                            AuthorizationsList.Add(item);
                        }
                    }

                }
            }
            else
            {
                foreach (var item in App.CashierData.GetAllAuthorizations())
                {
                    AuthorizationsListAll.Add(item);
                }
                if (MainPage.TodosAuth)
                {
                    MainPage.ToValidate = true;
                    MainPage.Authorized = true;
                    MainPage.Reject = true;
                }
                else
                {
                    if (MainPage.ToValidate)
                    {
                        foreach (var item in App.CashierData.GetAllAuthorizations().Where(r => r.Status == 0 && r.CheckDate > minDate && r.CheckDate < maxDate))
                        {
                            AuthorizationsList.Add(item);
                        }
                    }
                    if (MainPage.Authorized)
                    {
                        foreach (var item in App.CashierData.GetAllAuthorizations().Where(r => r.Status == 1 && r.CheckDate > minDate && r.CheckDate < maxDate))
                        {
                            AuthorizationsList.Add(item);
                        }
                    }
                    if (MainPage.Reject)
                    {
                        foreach (var item in App.CashierData.GetAllAuthorizations().Where(r => r.Status == 2 && r.CheckDate > minDate && r.CheckDate < maxDate))
                        {
                            AuthorizationsList.Add(item);
                        }
                    }

                }
            }
        }


    }
}
