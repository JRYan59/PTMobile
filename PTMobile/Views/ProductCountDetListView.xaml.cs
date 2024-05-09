using CommunityToolkit.Maui.Views;
using PTMobile.PopUp;
using PTMobile.ViewModel;

namespace PTMobile.Views;

public partial class ProductCountDetListView : ContentPage
{
    public ProductCountDetListView()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        this.Title = MainPage.CurrentProductName;
        this.BindingContext = new Product_Count_Dets(App.CashierData);
        MainPage.Product_Count_Det = new();
    }

    private async void ProductDetCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
            string respuesta = await DisplayActionSheet("Desea Editar el Registro?", "Cancelar", null, "Editar");

        if (respuesta == "Editar")
        {
            MainPage.Product_Count_Det = (Models.Product_Count_Det)e.CurrentSelection.FirstOrDefault();
            await AppShell.Current.GoToAsync(nameof(Product_Count_Det));
        }
        

    }
}