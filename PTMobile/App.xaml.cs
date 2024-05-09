using Plugin.Fingerprint;

namespace PTMobile;

public partial class App : Application
{
	public static CashierDatabase CashierData { get; set; }
	public App(CashierDatabase cashierDatabase)
	{
		InitializeComponent();
		string ruta = FileSystem.Current.AppDataDirectory;

		
        if (!Preferences.ContainsKey("UserInfo"))
		{

            MainPage = new LoginPage(CrossFingerprint.Current);

        }
		else
		{
			MainPage = new AppShell();
		}
            
		CashierData = cashierDatabase;
	}


	
}
