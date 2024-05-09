using BarcodeScanner.Mobile;
using CommunityToolkit.Maui;
using PTMobile.PopUp;
using PTMobile.ViewModel;
using PTMobile.Views;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;

#if ANDROID
using PTMobile.Platforms.Android;
#endif

namespace PTMobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
        var builder = MauiApp.CreateBuilder();
#if ANDROID
        builder.Services.AddTransient<IServicesTest, DemoServices>();
#endif
        builder

            .UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
				fonts.AddFont("Roboto-Black.ttf", "RobotoBlack");
			}).ConfigureMauiHandlers(handlers =>
            {
                // Add the handlers
                handlers.AddBarcodeScannerHandler();
            });



        string dbPath = FileAccessHelper.GetLocalFilePath("Cashiersdb1.db3");



	   builder.Services.AddSingleton<CashierDatabase>(s => ActivatorUtilities.CreateInstance<CashierDatabase>(s, dbPath));
        builder.Services.AddSingleton<IMediaPicker, CustomMediaPicker>();
		builder.Services.AddSingleton<GalleryViewModel>();
        builder.Services.AddSingleton<ProductFilter>();
        builder.Services.AddTransient<ProductFilter>();
        builder.Services.AddSingleton<ProductDetail>();

        return builder.Build();
	}


}
