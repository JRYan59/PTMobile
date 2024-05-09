namespace PTMobile.Views;

public partial class AuthorizationsFilterView : ContentPage
{
    public AuthorizationsFilterView()
	{
		InitializeComponent();
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        AutorizadosCheckBox.IsChecked = MainPage.Authorized;
        RechazadosCheckBox.IsChecked = MainPage.Reject;
        PorValidarCheckBox.IsChecked = MainPage.ToValidate;
        if (MainPage.FPorSolicitud)
        {
            FechaSolCheckBox.IsChecked = true;
            SolDateBegin.Date = MainPage.MinDate;
            SolDateEnd.Date = MainPage.MaxDate;

        }
        if (MainPage.FPorValidacion)
        {
            FechaValidacionCheckBox.IsChecked = true;
            ValidDateBegin.Date = MainPage.MinDate;
            ValidDateBegin.Date = MainPage.MaxDate;
        }           
        

        if (AutorizadosCheckBox.IsChecked && RechazadosCheckBox.IsChecked && PorValidarCheckBox.IsChecked)
        {
            TodosCheckBox.IsChecked = true;
        }
    }

    private void TodosCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (TodosCheckBox.IsChecked == true)
        {
            RechazadosCheckBox.IsChecked = true;
            PorValidarCheckBox.IsChecked = true;
            AutorizadosCheckBox.IsChecked = true;
        }
    }

    private void PorValidarCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (PorValidarCheckBox.IsChecked)
        {
            MainPage.ToValidate = true;
        }
        else
        {
            MainPage.ToValidate = false;
            this.TodosCheckBox.IsChecked = false;
        }
    }

    private void AutorizadosCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (AutorizadosCheckBox.IsChecked)
        {
            MainPage.Authorized = true;
        }
        else
        {
            MainPage.Authorized = false;
            this.TodosCheckBox.IsChecked = false;
        }
    }

    private void RechazadosCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (RechazadosCheckBox.IsChecked)
        {
            MainPage.Reject = true;
        }
        else
        {
            MainPage.Reject = false;
            this.TodosCheckBox.IsChecked = false;
        }
    }

    private void FechaSolCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value == true)
        {
            MainPage.FPorSolicitud = true;
            FechaValidacionCheckBox.IsChecked = false;
            MainPage.FPorValidacion= false;
        }
        else
        {
            MainPage.FPorSolicitud = false;
        }
    }

    private void FechaValidacionCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value == true)
        {
            MainPage.FPorValidacion = true;
            FechaSolCheckBox.IsChecked = false;
            MainPage.FPorSolicitud= false;
        }
        else
        {
            MainPage.FPorValidacion = false;
        }
    }

    private void SolDateBegin_DateSelected(object sender, DateChangedEventArgs e)
    {
        MainPage.MinDate = e.NewDate;
    }

    private void SolDateEnd_DateSelected(object sender, DateChangedEventArgs e)
    {
        MainPage.MaxDate = e.NewDate;
    }

    private void ValidDateBegin_DateSelected(object sender, DateChangedEventArgs e)
    {
        MainPage.MinDate = e.NewDate;
    }

    private void ValidDateEnd_DateSelected(object sender, DateChangedEventArgs e)
    {
        MainPage.MaxDate = e.NewDate;
    }

    private async void FiltrarBtn_Clicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("..");
    }

    private void LimpiarFiltroBtn_Clicked(object sender, EventArgs e)
    {
        this.RechazadosCheckBox.IsChecked = false;
        this.AutorizadosCheckBox.IsChecked = false;
        this.PorValidarCheckBox.IsChecked = true;
        this.FechaSolCheckBox.IsChecked = false;
        this.FechaValidacionCheckBox.IsChecked = false;

    }
}