using Mobile_App.ViewModels;

namespace Mobile_App.Views;

public partial class ScannerPage : ContentPage
{
	public ScannerPage(ScannerViewModel scannerViewModel)
	{
		InitializeComponent();
        this.BindingContext = scannerViewModel;
    }

	private void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
	{
        Dispatcher.Dispatch(() =>
        {
            barcodeResult.Text = $"{e.Results[0].Value} {e.Results[0].Format}";
        });
    }
}