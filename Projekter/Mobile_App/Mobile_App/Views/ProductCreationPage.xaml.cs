using Mobile_App.ViewModels;

namespace Mobile_App.Views;

public partial class ProductCreationPage : ContentPage
{
	public ProductCreationPage(ProductViewModel productViewModel)
	{
		InitializeComponent();
        this.BindingContext = productViewModel;
    }
}