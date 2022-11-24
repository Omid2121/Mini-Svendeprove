using Mobile_App.ViewModels;

namespace Mobile_App.Views;

public partial class SalePage : ContentPage
{
	public SalePage(SaleViewModel saleViewModel)
	{
        InitializeComponent();
        this.BindingContext = saleViewModel;
	}
}