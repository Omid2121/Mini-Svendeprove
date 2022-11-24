using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Mobile_App.Services;
using Mobile_App.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VareskanningModels;
using VareskanningModels.SQL;

namespace Mobile_App.ViewModels
{
    public partial class SaleViewModel : BaseViewModel
    {
        [ObservableProperty]
        private DateTimeOffset _timestamp;
        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _barcode;
        [ObservableProperty]
        private string _selectedType;
        [ObservableProperty]
        private string _selectedProductType;
        [ObservableProperty]
        private decimal _price;

        [ObservableProperty]
        private List<SaleDTO> _salesDTO;

        readonly SaleService saleService = new();
        public List<string> ProductTypes { get; } = Enum.GetNames(typeof(ProductType)).ToList();

        public SaleViewModel()
        {
            GetSales();
        }

        [ICommand]
        public async void GetSales()
        {
            try
            {
                SalesDTO = (List<SaleDTO>) await saleService.GetSalesAsync();
                foreach (var saleDTO in SalesDTO)
                {
                    SelectedType = Enum.GetName(typeof(ProductType), saleDTO.ProductType);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        [ICommand]
        public async void AddSale()
        {
            if (!string.IsNullOrWhiteSpace(Barcode))
            {
                if (Preferences.ContainsKey(nameof(App.User)))
                {
                    await saleService.AddItemAsync(new PartialSale
                    {
                        Barcode = Barcode,
                        UserId = Preferences.Get(nameof(App.User), App.User.Id)
                        //UserId = App.User.Id
                    });
                }
            }            
        }

        [ICommand]
        public async void DeleteSale(Sale sale)
        {
            await saleService.DeleteItemAsync(sale.Id);
        }

        /// <summary>
        /// This method is called when user presses the ContinueCommand on SalePage.
        /// </summary>
        public override async void Continue()
        {
            await Shell.Current.GoToAsync(nameof(ScannerPage));
        }
    }
}
