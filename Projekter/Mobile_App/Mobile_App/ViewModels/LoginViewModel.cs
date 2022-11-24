using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Mobile_App.Services;
using Mobile_App.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VareskanningModels.SQL;

namespace Mobile_App.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {        
        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private string _password;

        readonly ILoginRepository loginRepository = new LoginService();
        
        [ICommand]
        public async void Login()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                User user = await loginRepository.Login(Username, Password);

                if (Preferences.ContainsKey(nameof(App.User)))
                {
                    Preferences.Remove(nameof(App.User));
                }
                
                string userDetails = JsonConvert.SerializeObject(user);
                Preferences.Set(nameof(App.User), userDetails);
                App.User = user;

                await Shell.Current.GoToAsync($"//{nameof(OverviewPage)}");
            }
        }

        [ICommand]
        public async void SignUp()
        {
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                User user = await loginRepository.SignUp(Username, Password);

                if (Preferences.ContainsKey(nameof(App.User)))
                {
                    Preferences.Remove(nameof(App.User));
                }

                string userDetails = JsonConvert.SerializeObject(user);
                Preferences.Set(nameof(App.User), userDetails);
                App.User = user;

                await Shell.Current.GoToAsync($"//{nameof(OverviewPage)}");
            }
        }

        public override async void Continue()
        {
            await Shell.Current.GoToAsync(nameof(SignUpPage));
        }
        public override void Previous()
        {
            base.Previous();
        }
    }
}
