using BarcodeScanner.Mobile;
using PTMobile.Models;

#if ANDROID
using Android.Hardware;
#endif

namespace PTMobile.Views;

    public partial class BarcodeScannerView : ContentPage
{
     

	public BarcodeScannerView()
	{
        InitializeComponent();
#if ANDROID
        Android.Hardware.Camera.CameraInfo cameraInfo = new Android.Hardware.Camera.CameraInfo();
        int numberOfCameras = Android.Hardware.Camera.NumberOfCameras;

        bool hasrear = false;
        for (int i = 0; i < numberOfCameras; i++)
        {
            Android.Hardware.Camera.GetCameraInfo(i, cameraInfo);
            if(cameraInfo.Facing == Android.Hardware.CameraFacing.Back)
            {
                hasrear = true;
                break;
            }

        }

        if (!hasrear)
            Camera.CameraFacing = BarcodeScanner.Mobile.CameraFacing.Front;
#endif
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
        MainPage.PrevPage = "";
    }
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        PermissionStatus status = await Permissions.RequestAsync<Permissions.Camera>();
        Camera.IsScanning = true;
        MainPage.Product_Count_Det = new Models.Product_Count_Det();
        MainPage.CurrentProductCode = "";

    }


    async void Camera_OnDetected(object sender, BarcodeScanner.Mobile.OnDetectedEventArg e)
    {
        List<BarcodeResult> obj = e.BarcodeResults;

        string result = string.Empty;
        for (int i = 0; i < obj.Count; i++)
        {
            result += $"{obj[i].DisplayValue}";
        }

        Dispatcher.Dispatch(async () =>
        {
            string respuesta = await DisplayActionSheet("Codigo: " + result, "Aceptar", null, "Reintentar");
            if (respuesta == "Aceptar")
            {
                if (MainPage.PrevPage == "ProductFilter")
                {
                    MainPage.CurrentProductCode = result;
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    MainPage.CurrentProductCode = result;
                    await Shell.Current.GoToAsync(nameof(Product_Count_Det));
                }
               
            }
            else
            {
                Camera.IsScanning = true;
            }
        });

    }

    async void SalirBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//TakingInventory",false);
    }

    async void ManualBtn_Clicked(object sender, EventArgs e)
    {
        MainPage.CurrentProductCode = "";
        await Shell.Current.GoToAsync(nameof(Product_Count_Det));
    }
}