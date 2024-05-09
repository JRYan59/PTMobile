using CommunityToolkit.Maui.Views;
using PTMobile.Models;
using PTMobile.PopUp;
using PTMobile.ViewModel;

namespace PTMobile;

public partial class CashierFilter : ContentPage
{
	DateTime Desde = DateTime.Today;
	DateTime Hasta = DateTime.Today;
	List<string> Cashier = new List<string>();	
	

	public CashierFilter()
	{

		InitializeComponent();
		Cashier = MainPage.Cashier;

		desde.DateSelected += Desde_DateSelected;
		hasta.DateSelected += Hasta_DateSelected;

		//if (DeviceDisplay.Current.MainDisplayInfo.Width < DeviceDisplay.Current.MainDisplayInfo.Height)
		//{
		//	fechas.Margin = new Thickness(0, -270, 0, 0);
		//	btnCajas.Margin = new Thickness(0, -630, 0, 0);
		//}

		DeviceDisplay.Current.MainDisplayInfoChanged += Current_MainDisplayInfoChanged;

    }

    private void Desde_DateSelected(object sender, DateChangedEventArgs e)
    {
		Desde = e.NewDate;
        
    }
	
    private void Hasta_DateSelected(object sender, DateChangedEventArgs e)
	{
		Hasta = e.NewDate;
        
    }



	private void Current_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
	{
		//if(DeviceDisplay.Current.MainDisplayInfo.Width> DeviceDisplay.Current.MainDisplayInfo.Height)
		//{
  //          fechas.Margin = new Thickness(0, -30, 0, 0);
		//	btnCajas.Margin = new Thickness(0, -30, 0, 0);
		//	return;
  //      }
		//else
		//{
		//	//fechas.Margin = new Thickness(0, -270, 0, 0);
  // //         btnCajas.Margin = new Thickness(0, -630, 0, 0);
		//	//return;

  //      }
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		this.ShowPopup(new CashierPopUp());
	}

	private void btnFilter_Clicked(object sender, EventArgs e)
	{

        this.BindingContext = new Reports(Desde, Hasta, Cashier);
		TitleDate.IsVisible = true;
        ResultTitle.IsVisible = true;

    }

	async void BtnOpenRep_Clicked(object sender, EventArgs e)
	{
		try
		{
            if (!string.IsNullOrEmpty(((ReportZ)reports.SelectedItem).TotalClass))
            {
                MainPage.FinalReport = ((ReportZ)reports.SelectedItem).TotalClass;

                if (MainPage.FinalReport is not null && MainPage.FinalReport != string.Empty)
                {

                    await Shell.Current.GoToAsync(nameof(Report));
                }
            }
        }
		catch (Exception ex)
		{

			await DisplayAlert("Error", "No se ha seleccionado ningun Reporte", "Aceptar");
		}
		
        
		

    }
}