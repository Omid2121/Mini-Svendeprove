using Mobile_App.ViewModels;

namespace Mobile_App.Views;

public partial class SignUpPage : ContentPage
{
    public SignUpPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        this.BindingContext = loginViewModel;
    }
}