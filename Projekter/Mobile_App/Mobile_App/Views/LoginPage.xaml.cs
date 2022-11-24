using Mobile_App.ViewModels;

namespace Mobile_App.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
        this.BindingContext = loginViewModel;
    }
}