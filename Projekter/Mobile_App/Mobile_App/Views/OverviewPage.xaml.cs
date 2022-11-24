using Mobile_App.ViewModels;

namespace Mobile_App.Views;

public partial class OverviewPage : ContentPage
{
	public OverviewPage(OverviewViewModel overviewViewModel)
	{
		InitializeComponent();
        this.BindingContext = overviewViewModel;
    }
}