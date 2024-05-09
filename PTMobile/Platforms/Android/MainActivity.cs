using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.Fingerprint;
using PTMobile.Functions;

namespace PTMobile;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    internal static readonly string Channel_ID = "TestChannel";
    internal static readonly int NotificationID = 101;

    public static MainActivity ActivityCurrent { get; set; }
    public MainActivity()
    {
        ActivityCurrent = this;
    }

    protected override void OnStop()
    {
        base.OnStop();
        MainPage.ftpThread = new Thread(() => MainPage.ftpPictures.Sync());
        if(ProductFilter.Services != null)
        ProductFilter.Services.Stop();
    }

    protected override void OnRestart()
    {
        base.OnRestart();
        if (ProductFilter.Services != null)
            ProductFilter.Services.Start();

    }
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        CrossFingerprint.SetCurrentActivityResolver(() => this);

        CreateNotificationChannel();

        Intent intent = this.Intent;
        AddIn.SetNavFromNotification(intent);

    }

    protected override void OnNewIntent(Intent intent)
    {
        base.OnNewIntent(intent);
    }



    private void CreateNotificationChannel()
    {
        if (OperatingSystem.IsOSPlatformVersionAtLeast("android", 26))
        {
            var channel = new NotificationChannel(Channel_ID, "Test Notification Channel", NotificationImportance.Default);

            var notificationManager = (NotificationManager)GetSystemService(Android.Content.Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
        
    }
}
