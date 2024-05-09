using CommunityToolkit.Maui.Views;
using PTMobile.Views;

namespace PTMobile.PopUp;

public partial class ProductCountPopUp : Popup
{
	public ProductCountPopUp()
	{
		InitializeComponent();
		if(MainPage.Product_Count != null && MainPage.Product_Count.Id > 0)
		{
            WareHouse.Text = MainPage.Product_Count.Warehouse;
            Descr.Text = MainPage.Product_Count.Descr;
        }
		
	}

	private async void SaveCountBtn_Clicked(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(WareHouse.Text) && !string.IsNullOrEmpty(Descr.Text))
		{
			
			if (MainPage.Product_Count != null && MainPage.Product_Count.Id > 0)
			{
				App.CashierData.EditProductCount(MainPage.Product_Count, Descr.Text, WareHouse.Text);
				MainPage.Product_Count = App.CashierData.GetProductCount(MainPage.Product_Count.Id);
				Close();
                await Shell.Current.GoToAsync(nameof(BarcodeScannerView));

            }
			else
			{
                MainPage.Product_Count = App.CashierData.GetProductCount(App.CashierData.StartProductCount(WareHouse.Text, Descr.Text));
                Close();
                await Shell.Current.GoToAsync(nameof(BarcodeScannerView));
            }

			
        }
		else
		{
			if (string.IsNullOrEmpty(WareHouse.Text))
				await Shell.Current.DisplayAlert("Almacen vacio", "Debe ingresar el almacen", "Volver");

			if (string.IsNullOrEmpty(Descr.Text))
				await Shell.Current.DisplayAlert("Descripcion vacia", "Debe ingresar descripcion del conteo", "Volver");
        }
	}
    private void CancelBtn_Clicked(object sender, EventArgs e)
    {
		Close();
    }
}