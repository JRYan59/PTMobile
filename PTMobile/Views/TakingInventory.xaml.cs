using CommunityToolkit.Maui.Views;
using PTMobile.Models;
using PTMobile.PopUp;
using PTMobile.ViewModel;

namespace PTMobile.Views;

public partial class TakingInventory : ContentPage
{
    public TakingInventory()
	{
		InitializeComponent();
        
		
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        MainPage.CurrentProductCode = "";
        MainPage.Product_Count = new();
        if(!string.IsNullOrEmpty(MainPage.WareHouse))
        CurrentWarehouseLbl.Text = MainPage.WareHouse;

        if (MainPage.FPorInicio)
        {
            this.BindingContext = new Product_Counts(MainPage.MinDate, MainPage.MaxDate, true);
            return;
        }
        if (MainPage.FPorFinal)
        {
            this.BindingContext = new Product_Counts(MainPage.MinDate, MainPage.MaxDate, false);
            return;
        }
        this.BindingContext = new Product_Counts();


        
        
    }

    private void NewProductCountBtn_Clicked(object sender, EventArgs e)
	{
        this.ShowPopup(new ProductCountPopUp());
        //App.CashierData.StartProductCount();
	}

    async void ProductCountsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ProductCountsCollectionView.SelectedItem != null)
        {

            string respuesta = await DisplayActionSheet("Seleccione opcion", "Cancelar", null, "Editar", "Procesar", "Anular", "Ver Lista");
            if (respuesta == "Editar")
            {
                MainPage.Product_Count = (Product_Count)ProductCountsCollectionView.SelectedItem;
                this.ShowPopup(new ProductCountPopUp());

            }
            if (respuesta == "Procesar")
            {
                App.CashierData.UpdateProductCountStatus((Product_Count)ProductCountsCollectionView.SelectedItem, 1);
                this.BindingContext = new Product_Counts();
            }
            if (respuesta == "Anular")
            {
                App.CashierData.UpdateProductCountStatus((Product_Count)ProductCountsCollectionView.SelectedItem, 2);
                this.BindingContext = new Product_Counts();
            }
            if(respuesta == "Ver Lista")
            {
                MainPage.Product_Count = (Product_Count)ProductCountsCollectionView.SelectedItem;
                await AppShell.Current.GoToAsync(nameof(TakingInventoryDet));
            }
        }
        ProductCountsCollectionView.SelectedItem = null;
    }

    async void FilterProductCountBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TakingInventoryFilterView));
    }
}