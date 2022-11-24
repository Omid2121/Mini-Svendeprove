using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Mobile_App.Services;
using Mobile_App.Views;
using System.Windows.Input;
using VareskanningModels;
using VareskanningModels.SQL;

namespace Mobile_App.ViewModels
{
    public partial class ProductViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _barcode;
        [ObservableProperty]
        private string _selectedType;
        [ObservableProperty]
        private decimal _price;
        [ObservableProperty]
        private List<Product> _products;

        readonly ProductService productService = new();
        public List<string> ProductTypes { get; } = Enum.GetNames(typeof(ProductType)).ToList();

        public ProductViewModel()
        {
            GetProducts();
        }

        [ICommand]
        public  void GetProducts()
        {
            try
            {
                Products = (List<Product>) productService.GetItemsAsync().Result;
                // Doesn't work
                foreach (var product in Products)
                {
                    SelectedType = Enum.GetName(typeof(ProductType), product.ProductType);
                }
            }
            finally
            {
                IsBusy = false;
            }
            
        }

        [ICommand]
        public async void AddProduct()
        {
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Barcode))
            {
                await productService.AddItemAsync(new Product
                {
                    Name = Name,
                    Barcode = Barcode,
                    ProductType = Enum.Parse<ProductType>(SelectedType),
                    Price = Price
                });
            }
            await Shell.Current.GoToAsync($"//{nameof(ProductPage)}");
        }

        [ICommand]
        public async void DeleteProduct(Product product)
        {
            await productService.DeleteItemAsync(product.Id);
        }

        /// <summary>
        /// This method is called when user presses the ContinueCommand on ProductPage.
        /// </summary>
        public override async void Continue()
        {
            await Shell.Current.GoToAsync(nameof(ProductCreationPage));
        }
    }
}
