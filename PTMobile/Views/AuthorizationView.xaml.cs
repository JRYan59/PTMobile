using PTMobile.Models;

namespace PTMobile.Views;

public partial class AuthorizationView : ContentPage
{
	public AuthorizationView()
	{
		InitializeComponent();
		
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        this.BindingContext = MainPage.Authorization;
        if(MainPage.Authorization.Ended == false)
        {
            this.AuthorizeBtn.IsVisible = true;
            this.RejectBtn.IsVisible = true;
            this.FValidacionLbl.IsVisible = true;
        }
        
    }

    private void AuthorizeBtn_Clicked(object sender, EventArgs e)
    {
        Models.Authorization au = MainPage.Authorization;
        au.Status = 1;
        au.CheckDate = DateTime.Now;
        App.CashierData.UpdateAuthorization(au);

        Shell.Current.DisplayAlert("Autorizaci�n Aprobada!", "Se aprob� correctamente", "OK");
        Shell.Current.GoToAsync("..");
    }

    private void RejectBtn_Clicked(object sender, EventArgs e)
    {
        Models.Authorization au = MainPage.Authorization;
        au.Status = 2;
        au.CheckDate = DateTime.Now;
        App.CashierData.UpdateAuthorization(au);

        Shell.Current.DisplayAlert("Autorizaci�n Rechazada!", "Se rechaz� correctamente", "OK");
        Shell.Current.GoToAsync("..");
    }
}