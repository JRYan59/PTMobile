using Plugin.Fingerprint;
using PTMobile.Views;


namespace PTMobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(CashierFilter), typeof(CashierFilter));
        Routing.RegisterRoute(nameof(Report), typeof(Report));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(AppShell), typeof(AppShell));
        Routing.RegisterRoute(nameof(App), typeof(App));
        Routing.RegisterRoute(nameof(ProductDetail), typeof(ProductDetail));
        Routing.RegisterRoute(nameof(ProductFilter), typeof(ProductFilter));
        Routing.RegisterRoute(nameof(Product_Count_Det), typeof(Product_Count_Det));
        Routing.RegisterRoute(nameof(BarcodeScannerView), typeof(BarcodeScannerView));
        Routing.RegisterRoute(nameof(ProductCountDetListView), typeof(ProductCountDetListView));
        Routing.RegisterRoute(nameof(TakingInventoryDet), typeof(TakingInventoryDet));
        Routing.RegisterRoute(nameof(AuthorizationView), typeof(AuthorizationView));
        Routing.RegisterRoute(nameof(AuthorizationsFilterView), typeof(AuthorizationsFilterView));
        Routing.RegisterRoute(nameof(TakingInventoryFilterView), typeof(TakingInventoryFilterView));



    }
    private void LogoutBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Remove("UserInfo");
        Application.Current.MainPage = new LoginPage(CrossFingerprint.Current);
        //await Shell.Current.GoToAsync(nameof(LoginPage));

    }
}
