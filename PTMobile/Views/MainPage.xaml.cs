
using CommunityToolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using PTDocsClasses;
using PTMobile.Models;
using PTMobile.Views;
using System.Collections.Generic;
using System.Text;

namespace PTMobile;

public partial class MainPage : ContentPage
{
	public static List<string> Cashier = new List<string>();
	public static string FinalReport;
	public static bool forDescr = false;
	public static bool forCode = false;
    public static bool All = false;
	public static Models.Product Product;
	public static User CurrentUser = new User();
	public static Product_Count Product_Count;
	public static Models.Product_Count_Det Product_Count_Det;
	public static string CurrentProductCode;
    public static string CurrentProductName;
    public static Models.Authorization Authorization;


    public static bool Todos = false;
    public static bool InProgress = true;
    public static bool Canceled = false;
    public static bool Processed = false;
	public static string WareHouse = "";
    public static bool FPorInicio = false;
    public static bool FPorFinal = false;


    public static bool TodosAuth = false;
    public static bool ToValidate = true;
    public static bool Reject = false;
    public static bool Authorized = false;
    public static bool FPorSolicitud = false;
    public static bool FPorValidacion = false;
	public static DateTime MinDate = DateTime.Now;
    public static DateTime MaxDate = DateTime.Now;

    private string _devicetoken;

	public static string PrevPage;

   public static FTPService ftpPictures = new FTPService("usuarioftp", "123ftp456*", "192.168.0.100", 21);

   public static Thread ftpThread = new Thread(() => ftpPictures.Sync());

    public MainPage()
	{
		InitializeComponent();
        WeakReferenceMessenger.Default.Register<PushNotificationReceived>(this, (r, m) =>
        {
            string msg = m.Value;
            NavigateToPage();
        });
        if (Preferences.ContainsKey("DeviceToken"))
        {
            _devicetoken = Preferences.Get("DeviceToken", "");
        }
        NavigateToPage();


    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        base.OnNavigatedFrom(args);
    }
    public void NavigateToPage()
    {
      
        

        if (Preferences.ContainsKey("NavigationID"))
        {
            string id = Preferences.Get("NavigationID", "");
            if (id == "1")
            {
                Shell.Current.DisplayAlert("Funciona", "Funciona", "OK");
            }
            else
            {
                Shell.Current.DisplayAlert("No encuentra ID", "NO", "OK");
            }
            Preferences.Remove("NavigationID");
        }
        else
        {
            Shell.Current.DisplayAlert("No encuentra NavigationID", "NO", "OK");
        }
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        MainPage.CurrentProductCode = "";


    }
    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
    }


    private async void PruebaNotificaciones_Clicked(object sender, EventArgs e)
    {

        var androidNotificationObject = new Dictionary<string, string>();
		androidNotificationObject.Add("NavigationID", "1");

		var pushNotificationRequest = new Models.PushNotificationRequest
		{
			notification = new NotificationMessageBody
			{
				title = "Notification Title",
				body = "Notification body"
			},
			data = androidNotificationObject,
			registration_ids = new List<string> { _devicetoken }
		};
		string url = "https://fcm.googleapis.com/fcm/send";

		using(var client = new HttpClient())
		{
			client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key","="+ "AAAATWca6IE:APA91bHECcMc9oRHMNM16noupwiObWhCpKvP8cnlYWI9Yw8OSwQ-HW2liIe4CpMqYu3uqMhKh55ww0cXiQFuFhCCLydDBizbc1oQRnOaPatiNK9czR1OghQa929HVnd2e7ToriFfPfem");
			string serializeRequest = JsonConvert.SerializeObject(pushNotificationRequest);
			var response = await client.PostAsync(url, new StringContent(serializeRequest,Encoding.UTF8,"application/json"));
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				await App.Current.MainPage.DisplayAlert("Notification Sent", "Notification Sent", "OK");
			}
		}
    }

    
}