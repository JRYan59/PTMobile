using PTMobile.ViewModel;

namespace PTMobile.Views;

public partial class TakingInventoryFilterView : ContentPage
{
	public TakingInventoryFilterView()
	{
		InitializeComponent();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        EnProcesoCheckBox.IsChecked = MainPage.InProgress;
        AnuladoCheckBox.IsChecked = MainPage.Canceled;
        ProcesadoCheckBox.IsChecked = MainPage.Processed;

        WareHousePicker.BindingContext = new Product_Counts();

        if (MainPage.FPorInicio)
        {
            StartDateCheck.IsChecked = true;
            StartDatePickerBegin.Date = MainPage.MinDate;
            StartDatePickerFinish.Date = MainPage.MaxDate;
        }
        if (MainPage.FPorFinal)
        {
            EndDateCheck.IsChecked = true;
            EndDatePickerBegin.Date = MainPage.MinDate;
            EndDatePickerFinish.Date = MainPage.MaxDate;
        }
        if(EnProcesoCheckBox.IsChecked && AnuladoCheckBox.IsChecked && ProcesadoCheckBox.IsChecked)
        {
            TodosCheckBox.IsChecked = true;
        }
    }

    private void TodosCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (TodosCheckBox.IsChecked == true)
        {
            AnuladoCheckBox.IsChecked = true;
            EnProcesoCheckBox.IsChecked = true;
            ProcesadoCheckBox.IsChecked = true;
        }
    }

    private void EnProcesoCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (EnProcesoCheckBox.IsChecked)
        {
            MainPage.InProgress = true;
        }
        else
        {
            MainPage.InProgress = false;
            this.TodosCheckBox.IsChecked = false;
        }
    }

    private void ProcesadoCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (ProcesadoCheckBox.IsChecked)
        {
            MainPage.Processed = true;
        }
        else
        {
            MainPage.Processed = false;
            this.TodosCheckBox.IsChecked = false;
        }
    }

    private void AnuladoCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (AnuladoCheckBox.IsChecked)
        {
            MainPage.Canceled = true;
        }
        else
        {
            MainPage.Canceled = false;
            this.TodosCheckBox.IsChecked = false;
        }
    }

    private void FiltrarBtn_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (WareHousePicker.SelectedItem != null)
        {
            MainPage.WareHouse = ((Models.Product_Count)WareHousePicker.SelectedItem).Warehouse;
        }

    }

    private void LimpiarFiltroBtn_Clicked(object sender, EventArgs e)
    {
        MainPage.WareHouse = "";
        this.ProcesadoCheckBox.IsChecked = false;
        this.AnuladoCheckBox.IsChecked = false;
        this.EnProcesoCheckBox.IsChecked = true;
        this.StartDateCheck.IsChecked = false;
        this.EndDateCheck.IsChecked = false;
    }

    private void StartDateCheck_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value == true)
        {
            
            MainPage.FPorInicio = true;
            EndDateCheck.IsChecked = false;
            MainPage.FPorFinal = false;
        }
        else
        {
            MainPage.FPorInicio = false;
        }
    }

    private void EndDateCheck_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value == true)
        {
            StartDateCheck.IsChecked = false;
            MainPage.FPorFinal = true;
            MainPage.FPorInicio = false;
        }
        else
        {
            MainPage.FPorFinal = false;
        }
    }

    private void StartDatePickerBegin_DateSelected(object sender, DateChangedEventArgs e)
    {
        MainPage.MinDate = e.NewDate;
    }

    private void EndDatePickerBegin_DateSelected(object sender, DateChangedEventArgs e)
    {
        MainPage.MaxDate = e.NewDate;
    }

    private void StartDatePickerFinish_DateSelected(object sender, DateChangedEventArgs e)
    {
        MainPage.MinDate = e.NewDate;
    }

    private void EndDatePickerFinish_DateSelected(object sender, DateChangedEventArgs e)
    {
        MainPage.MaxDate = e.NewDate;
    }
}