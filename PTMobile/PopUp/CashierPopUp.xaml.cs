using CommunityToolkit.Maui.Views;
using PTMobile.Models;
using PTMobile.ViewModel;

namespace PTMobile.PopUp;

public partial class CashierPopUp : Popup
{


    List<Cashier> SelectedCashiers = new List<Cashier>();

	public CashierPopUp()
	{
        InitializeComponent();
		
		this.BindingContext = new Cajas();
    }

	public void cajas_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		MainPage.Cashier.Clear();
		SelectedCashiers.Clear();
		foreach (Cashier item in e.CurrentSelection)
		{
			SelectedCashiers.Add(item);
		}
	}

	public void Aceptar_Clicked(object sender, EventArgs e)
	{
        foreach (var item in SelectedCashiers)
        {
			//await App.CashierData.AddNewCashier(item);
			MainPage.Cashier.Add(item.Id);
        }

		Close();
    }
}