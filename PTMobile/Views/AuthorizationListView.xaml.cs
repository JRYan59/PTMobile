using PTMobile.ViewModel;

namespace PTMobile.Views;

public partial class AuthorizationListView : ContentPage
{
    
    public AuthorizationListView()
	{
		InitializeComponent();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (MainPage.FPorSolicitud)
        {
            this.BindingContext = new Authorizations(MainPage.MinDate, MainPage.MaxDate, false);
            return;
        }
        if (MainPage.FPorValidacion)
        {
            this.BindingContext = new Authorizations(MainPage.MinDate, MainPage.MaxDate, true);
            return;
        }
        this.BindingContext = new Authorizations();
    }

    async void AuthorizationCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        MainPage.Authorization = new();
        if (AuthorizationCollectionView.SelectedItem != null)
        {
            MainPage.Authorization = (Models.Authorization)AuthorizationCollectionView.SelectedItem;
            await Shell.Current.GoToAsync(nameof(AuthorizationView));
        }
    }

    private void FilterBtn_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AuthorizationsFilterView));
    }
}