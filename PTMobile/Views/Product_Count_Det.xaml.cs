

using PTMobile.Models;
using static SQLite.SQLite3;

namespace PTMobile.Views;

public partial class Product_Count_Det : ContentPage
{


    public Product_Count_Det()
	{
		InitializeComponent();
        //BarcodeScanner.Mobile.Methods.AskForRequiredPermission();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        
        base.OnNavigatedTo(args);
        MainPage.CurrentProductName = "";

        if (MainPage.Product_Count_Det.Id > 0)
        {
            
            Code.Text = MainPage.Product_Count_Det.ProductCode;
            Descr.Text = App.CashierData.GetProduct(MainPage.Product_Count_Det.ProductCode).Name;
            this.Title = Descr.Text;
            Qty.Text = MainPage.Product_Count_Det.Qty.ToString();
            Location.Text = MainPage.Product_Count_Det.Location;
        }
        else
        {
            Code.Focus();
            if (!string.IsNullOrEmpty(MainPage.CurrentProductCode))
            {
                Code.Text = MainPage.CurrentProductCode;
                Code_Completed(Code, null);
            }
        };
       
    }

    async void SalirBtn_Clicked(object sender, EventArgs e)
    {
       await Shell.Current.GoToAsync("//TakingInventory", false);

    }

    async void GuardarSalirBtn_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Code.Text) && !string.IsNullOrEmpty(Location.Text) && !string.IsNullOrEmpty(Descr.Text)&& !string.IsNullOrEmpty(Qty.Text))
        {


            if (MainPage.Product_Count_Det.Id > 0)
            {
                MainPage.Product_Count_Det.Qty = Int32.Parse(Qty.Text);
                MainPage.Product_Count_Det.Location = Location.Text;
                MainPage.Product_Count_Det.Date = DateTime.Now;
                if (App.CashierData.UpdateProductCountDet(MainPage.Product_Count_Det) > 0)
                {
                    await DisplayAlert("Guardado", "Se Guardó Correctamente", "OK");
                    await Shell.Current.GoToAsync("//TakingInventory", false);
                }


            }
            else
            {
                Models.Product_Count_Det pcd = new Models.Product_Count_Det();
                pcd.Id_Pc = MainPage.Product_Count.Id;
                pcd.Qty = Int32.Parse(Qty.Text);
                pcd.Location = Location.Text;
                pcd.Date = DateTime.Now;
                pcd.ProductCode = Code.Text;



                if (App.CashierData.SaveProductCountDet(pcd) > 0)
                {
                    await DisplayAlert("Guardado", "Se Guardó Correctamente", "OK");
                    await Shell.Current.GoToAsync("//TakingInventory", false);
                }
            }

        }
    }

    async void GuardarContinuarBtn_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Code.Text) && !string.IsNullOrEmpty(Location.Text) && !string.IsNullOrEmpty(Descr.Text) && !string.IsNullOrEmpty(Qty.Text))
        {
            
            if (MainPage.Product_Count_Det.Id > 0)
            {
                MainPage.Product_Count_Det.Qty = Int32.Parse(Qty.Text);
                MainPage.Product_Count_Det.Location = Location.Text;
                MainPage.Product_Count_Det.Date = DateTime.Now;
                if (App.CashierData.UpdateProductCountDet(MainPage.Product_Count_Det) > 0)
                {
                    await DisplayAlert("Guardado", "Se Guardó Correctamente", "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
            else
            {
                Models.Product_Count_Det pcd = new Models.Product_Count_Det();
                pcd.Id_Pc = MainPage.Product_Count.Id;
                pcd.Qty = Convert.ToInt32(Qty.Text);
                pcd.Location = Location.Text;
                pcd.Date = DateTime.Now;
                pcd.ProductCode = Code.Text;
                if (App.CashierData.SaveProductCountDet(pcd) > 0)
                {
                    await DisplayAlert("Guardado", "Se Guardó Correctamente", "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
        }
        
        
    }

    //async void Code_Unfocused(object sender, FocusEventArgs e)
    //{
        
        
    //}

    async void Code_Completed(object sender, EventArgs e)
    {
        Descr.Text = "";
        if (Code.Text.Length > 0)
        {
            
            Product currentProduct = App.CashierData.GetProduct(Code.Text);
            if (currentProduct != null)
            {
                this.Title = currentProduct.Name;
                if(App.CashierData.GetProduct_Count_Dets(MainPage.Product_Count.Id, Code.Text).Count() > 0)
                {
                    string respuesta = await DisplayActionSheet("Ya existe un conteo de este producto", "Cancelar", null, "Agregar", "Ver Lista De Conteos");
                    if (respuesta == "Agregar")
                    {
                        Descr.Text = currentProduct.Name;
                        Qty.Focus();
                    }
                    if(respuesta =="Ver Lista De Conteos")
                    {
                        MainPage.CurrentProductName = currentProduct.Name;
                        MainPage.CurrentProductCode = currentProduct.Code;
                        
                        await Shell.Current.GoToAsync(nameof(ProductCountDetListView));
                    }
                    if (respuesta == "Cancelar")
                    {
                        Code.Text = "";
                        Code.Focus();
                    }
                    
                }
                else
                {
                    Descr.Text = currentProduct.Name;
                    Qty.Focus();
                }
                
            }
            else
            {
                string respuesta = await DisplayActionSheet("El producto no existe", "Reintentar", null, "Crear Nuevo");
                if (respuesta == "Crear Nuevo")
                {
                    MainPage.Product = new Product();
                    MainPage.Product.Code = Code.Text;
                    await Shell.Current.GoToAsync(nameof(ProductDetail));
                }
                else
                {
                    Code.Focus();
                }
            }
        }
        else
        {
            await DisplayAlert("Codigo Vacio", "Debe Ingresar Un Codigo", "Aceptar");
            Code.Focus();
        }
    }

    async void ScanBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(BarcodeScannerView));
    }
}