using Mobile_App.ViewModels;
using Mobile_App.Views;
using Syncfusion.Maui.Core.Hosting;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace Mobile_App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .ConfigureSyncfusionCore()
            .UseBarcodeReader()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})

        .ConfigureMauiHandlers(h =>
        {
            h.AddHandler(typeof
                (CameraBarcodeReaderView),
                typeof(CameraBarcodeReaderViewHandler));
            h.AddHandler(typeof
                (CameraView),
                typeof(CameraViewHandler));
            h.AddHandler(typeof
                (BarcodeGeneratorView),
                typeof(BarcodeGeneratorViewHandler));
        });

        // Singleton for View Pages
        builder.Services.AddSingleton<OverviewPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<SignUpPage>();
        builder.Services.AddSingleton<ProductPage>();
        builder.Services.AddSingleton<ProductCreationPage>();
        builder.Services.AddSingleton<SalePage>();
        builder.Services.AddSingleton<ScannerPage>();

        // Singleton for ViewModels
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<OverviewViewModel>();
        builder.Services.AddSingleton<ProductViewModel>();
        builder.Services.AddSingleton<SaleViewModel>();
        builder.Services.AddSingleton<ScannerViewModel>();

        return builder.Build();
    }
}
