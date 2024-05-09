using CommunityToolkit.Maui.Views;
using PTMobile.PopUp;
using PTMobile.ViewModel;

namespace PTMobile.Views;

public partial class TakingInventoryDet : ContentPage
{
	public TakingInventoryDet()
	{
		InitializeComponent();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        MainPage.Product_Count_Det = new();
        this.Title = MainPage.Product_Count.Descr;
        this.BindingContext = new TakingInventoryDets(App.CashierData);
    }

    async void ProductDetCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string respuesta = await DisplayActionSheet("Desea Editar el Registro?", "Cancelar", null, "Editar");

        if (respuesta == "Editar")
        {
            Models.Product_Count_Det currentpcd = new Models.Product_Count_Det();
            currentpcd = ProductDetCollectionView.SelectedItem as Models.Product_Count_Det;


            MainPage.Product_Count_Det = currentpcd;
            await AppShell.Current.GoToAsync(nameof(Product_Count_Det));
        }
    }
}