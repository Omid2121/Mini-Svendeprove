using Mobile_App.ViewModels;

namespace Mobile_App.Views;

public partial class ProductPage : ContentPage
{
	public ProductPage(ProductViewModel productViewModel)
	{
		InitializeComponent();
        this.BindingContext = productViewModel;
    }
}