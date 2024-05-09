using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using PTMobile.Functions;

namespace PTMobile.Views;

public partial class ConfigurationView : ContentPage
{
	public ConfigurationView()
	{
		InitializeComponent();
	}

    async void CargarDemoBtn_Clicked(object sender, EventArgs e)
    {
     string resp = await Shell.Current.DisplayActionSheet("Desea Cargar la Demo?(Se borraran sus datos actuales)",null ,null,"Aceptar", "Cancelar");

        if (resp =="Aceptar")
        {
            App.CashierData.ClearDatabase();
            App.CashierData.AddNewUser(new Models.User() { Id = 1, Password = "1", UserName = "Demo" });
            #region Productos

            App.CashierData.AddNewProduct(new Models.Product() { Name = "Harina Pan", Code = "811297641593", Price = 20.5 });
            App.CashierData.AddNewProduct(new Models.Product() { Name = "Harina de Trigo", Code = "566908348489", Price = 30 });
            App.CashierData.AddNewProduct(new Models.Product() { Name = "Mantequilla", Code = "267461344819", Price = 40 });
            App.CashierData.AddNewProduct(new Models.Product() { Name = "Mayonesa", Code = "267461344819", Price = 60 });
            App.CashierData.AddNewProduct(new Models.Product() { Name = "Salsa de Tomate", Code = "267461344819", Price = 58.2 });

            #endregion

            #region Cajas
            App.CashierData.AddNewCashier(new Models.Cashier() { Name = "Caja 01", Id = "1" });
            App.CashierData.AddNewCashier(new Models.Cashier() { Name = "Caja 02", Id = "2" });
            App.CashierData.AddNewCashier(new Models.Cashier() { Name = "Caja 03", Id = "3" });
            App.CashierData.AddNewCashier(new Models.Cashier() { Name = "Caja 04", Id = "4" });
            App.CashierData.AddNewCashier(new Models.Cashier() { Name = "Caja 05", Id = "5" });
            #endregion

            #region ProductCount
            App.CashierData.AddNewProductCount(new Models.Product_Count() { Descr = "Conteo 1", StartDate = new DateTime(2023, 04, 10), Status = 0, UserId = 1, Warehouse = "Almacen 1" });
            App.CashierData.AddNewProductCount(new Models.Product_Count() { Descr = "Conteo 2", StartDate = new DateTime(2023, 05, 11), Status = 1, EndDate = new DateTime(2023, 05, 12), UserId = 1, Warehouse = "Almacen 2" });
            App.CashierData.AddNewProductCount(new Models.Product_Count() { Descr = "Conteo 3", StartDate = new DateTime(2023, 06, 12), Status = 2, EndDate = new DateTime(2023, 06, 13), UserId = 1, Warehouse = "Almacen 3" });
            App.CashierData.AddNewProductCount(new Models.Product_Count() { Descr = "Conteo 4", StartDate = new DateTime(2023, 07, 20), Status = 0, UserId = 1, Warehouse = "Almacen 4" });
            App.CashierData.AddNewProductCount(new Models.Product_Count() { Descr = "Conteo 5", StartDate = new DateTime(2023, 08, 09), Status = 0, UserId = 1, Warehouse = "Almacen 5" });
            #endregion

            #region ProductCountDet
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 1, Location = "Dep 1", ProductCode = "1", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 1, Location = "Dep 2", ProductCode = "2", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 1, Location = "Dep 3", ProductCode = "3", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 1, Location = "Dep 4", ProductCode = "4", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 1, Location = "Dep 5", ProductCode = "5", Date = DateTime.Now });

            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 2, Location = "Dep 1", ProductCode = "1", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 2, Location = "Dep 2", ProductCode = "2", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 2, Location = "Dep 3", ProductCode = "3", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 2, Location = "Dep 4", ProductCode = "4", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 2, Location = "Dep 5", ProductCode = "5", Date = DateTime.Now });

            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 3, Location = "Dep 1", ProductCode = "1", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 3, Location = "Dep 2", ProductCode = "2", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 3, Location = "Dep 3", ProductCode = "3", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 3, Location = "Dep 4", ProductCode = "4", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 3, Location = "Dep 5", ProductCode = "5", Date = DateTime.Now });

            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 4, Location = "Dep 1", ProductCode = "1", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 4, Location = "Dep 2", ProductCode = "2", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 4, Location = "Dep 3", ProductCode = "3", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 4, Location = "Dep 4", ProductCode = "4", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 4, Location = "Dep 5", ProductCode = "5", Date = DateTime.Now });

            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 5, Location = "Dep 1", ProductCode = "1", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 5, Location = "Dep 2", ProductCode = "2", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 5, Location = "Dep 3", ProductCode = "3", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 5, Location = "Dep 4", ProductCode = "4", Date = DateTime.Now });
            App.CashierData.SaveProductCountDet(new Models.Product_Count_Det() { Id_Pc = 5, Location = "Dep 5", ProductCode = "5", Date = DateTime.Now });
            #endregion

            #region Reports
            AddIn.FillDemoReports();
            #endregion

            #region Autorizaciones
            App.CashierData.AddNewAuthorization(new Models.Authorization() { Date = new DateTime(2023, 4, 2), Description = "Autorizacion 2", DetailDescription="Solicitud Autorizacion para anular documento", Code = "02", IdAuth = Guid.NewGuid().ToString(),Status = 1,UserId = "02", UserName = "User2",CheckDate= new DateTime(2023, 5, 9) });
            App.CashierData.AddNewAuthorization(new Models.Authorization() { Date = new DateTime(2023, 4, 3), Description = "Autorizacion 5", DetailDescription = "Solicitud Autorizacion para emitir Reporte Z", Code = "05", IdAuth = Guid.NewGuid().ToString(),Status = 2,UserId = "05", UserName = "User5",CheckDate= new DateTime(2023, 5, 11) });
            App.CashierData.AddNewAuthorization(new Models.Authorization() { Date = new DateTime(2023, 4, 4), Description = "Autorizacion 6", DetailDescription = "Solicitud Autorizacion para Ingresar al Sistema", Code = "06", IdAuth = Guid.NewGuid().ToString(), Status = 0, UserId = "03", UserName = "User3", CheckDate = new DateTime(2023, 5, 11) });
            #endregion

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            string text = "Se ha cargado la Data";
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);

        }


    }

    private void VerLogBtn_Clicked(object sender, EventArgs e)
    {
        Log log = new Log();
        string result = log.OpenLog(LogDatePicker.Date);
        if(result == "NE")
        {
            Shell.Current.DisplayAlert("Log no Existe", "Verifique fecha", "OK");
            LogView.Text = "";
        }
        else
        {
            LogView.Text = result;

        }
    }
}