using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using PTMobile.Models;
using PTMobile.ViewModel;

namespace PTMobile;

public partial class ProductFilter : ContentPage
{
    Product Selectedproduct;
    bool OrderByCode = true;
    bool OrderByDescr = false;

    public static IServicesTest Services;
    public ProductFilter(IServicesTest Services_)
    {
        InitializeComponent();
        ProductSearchBar.Text = "";
        AllButton_Clicked(AllButton, null);
        Services = Services_; 
    }
    public ProductFilter()
    {
		InitializeComponent();

        ProductSearchBar.Text = "";

        //App.CashierData.AddNewProduct(new Product() { Id= 3, Code = "7", Name = "Harina", Price = 1 });
        //App.CashierData.AddNewProduct(new Product() {Id=1, Code = "2", Name = "Producto 2", Price = 2 });
        //App.CashierData.AddNewProduct(new Product() { Id = 2, Code = "3", Name = "Producto 3", Price = 3 });

        //List<Product> products = new List<Product>();

        //products = App.CashierData.GetAllProducts();
        AllButton_Clicked(AllButton, null);
    }
	private void ProductSearchBar_TextChanged(object sender, TextChangedEventArgs e)
	{
		Entry entry = (Entry)sender;

        if (OrderByDescr)
        {
            this.BindingContext = new Products(entry.Text, 0);
            this.DescrTitle.BackgroundColor = new Color(130, 148, 96);

        }
        else
        {

            this.BindingContext = new Products(entry.Text, 1);
            this.CodTitle.BackgroundColor = new Color(130, 148, 96);
        }

        CodTitle.IsVisible=true;
        DescrTitle.IsVisible = true;
        PriceTitle.IsVisible = true;

        DetailButton.IsVisible=true;
        DetailButton.IsEnabled = false;
    }

	private void Nombre_Clicked(object sender, EventArgs e)
	{
		MainPage.forDescr = true;
        MainPage.forCode = false;
		MainPage.All = false;
        NameButton.BackgroundColor = new Color(130, 148, 96);
		CodeButton.BackgroundColor = new Color(67, 66, 66);
        AllButton.BackgroundColor = new Color(67, 66, 66);

        ProductSearchBar.Placeholder = PTMobile.Resources.Languages.AppResources.EnterName;

    }

	private void Code_Clicked(object sender, EventArgs e)
	{
        MainPage.forDescr = false;
		MainPage.forCode = true;
        MainPage.All = false;
        CodeButton.BackgroundColor = new Color(130, 148, 96);
		NameButton.BackgroundColor = new Color(67, 66, 66);
        AllButton.BackgroundColor = new Color(67, 66, 66);

        ProductSearchBar.Placeholder = PTMobile.Resources.Languages.AppResources.EnterCode;
    }

	private void AllButton_Clicked(object sender, EventArgs e)
	{
        MainPage.forDescr = false;
        MainPage.forCode = false;
        MainPage.All = true;
        AllButton.BackgroundColor = new Color(130, 148, 96);
        NameButton.BackgroundColor = new Color(67, 66, 66);
        CodeButton.BackgroundColor = new Color(67, 66, 66);
        ProductSearchBar.Placeholder = PTMobile.Resources.Languages.AppResources.EnterNameOrCode;
    }

    async void DetailButton_Clicked(object sender, EventArgs e)
    {
        MainPage.Product = Selectedproduct;

        await Shell.Current.GoToAsync(nameof(ProductDetail));
        
    }

    private void products_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Selectedproduct = new Product();
        MainPage.Product = new Product();
        Selectedproduct =(Product)e.CurrentSelection.FirstOrDefault();
        DetailButton.IsEnabled = true;
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {

        //base.OnNavigatedTo(args);

        this.BindingContext = new Products("",1);
        ProductSearchBar.Text = MainPage.CurrentProductCode;
        if (Services!=null)
            Services.Start();
        else
        {
            MainPage.ftpThread.Start();
        }
    }

    async void AddProductBtn_Clicked(object sender, EventArgs e)
    {
        MainPage.Product = new Product()
        {
            Name = ProductSearchBar.Text
        };
        await Shell.Current.GoToAsync(nameof(ProductDetail));
        
    }

    async void DeleteProductBtn_Clicked(object sender, EventArgs e)
    {
        var response = await Shell.Current.DisplayActionSheet("¿Desea eliminar el registro?", "", null, "Confirmar", "Cancelar");
        if (response == "Confirmar")
        {
            App.CashierData.DeleteProduct(Selectedproduct);
            ProductSearchBar_TextChanged(ProductSearchBar, null);
            ProductSearchBar.Text = "";

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            string text = "Producto eliminado";
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }
    }

    private void CodTitle_Clicked(object sender, EventArgs e)
    {
        if (OrderByDescr)
        {
            OrderByDescr = false;
            this.DescrTitle.BackgroundColor = new Color(67, 66, 66);

        }
        this.CodTitle.BackgroundColor = new Color(130, 148, 96);
        OrderByCode = true;
    }

    private void DescrTitle_Clicked(object sender, EventArgs e)
    {
        if (OrderByCode)
        {
            OrderByCode = false;
            this.CodTitle.BackgroundColor = new Color(67, 66, 66);

        }
        this.DescrTitle.BackgroundColor = new Color(130, 148, 96);
        OrderByDescr = true;
    }

    async void BarCodeBtn_Clicked(object sender, EventArgs e)
    {
        
        MainPage.PrevPage = "ProductFilter";
        await Shell.Current.GoToAsync(nameof(Views.BarcodeScannerView));
    }
}