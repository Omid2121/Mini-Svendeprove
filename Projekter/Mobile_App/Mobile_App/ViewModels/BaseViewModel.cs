using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Mobile_App.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;
        [ObservableProperty]
        private string _title;

        [ICommand]
        virtual public async void Continue()
        {
            throw new NotImplementedException();
        }

        [ICommand]
        virtual public async void Previous()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
