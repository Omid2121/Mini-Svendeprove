using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Mobile_App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VareskanningModels;

namespace Mobile_App.ViewModels
{
    public partial class OverviewViewModel : BaseViewModel
    {
        [ObservableProperty]
        Dictionary<string, int> _sales = new();
        [ObservableProperty]
        private List<SaleDTO> _salesDTO;

        readonly SaleService saleService = new();

        public OverviewViewModel()
        {
            GetSalesForChart();
        }

        [ICommand]
        public void GetSalesForChart()
        {
            SalesDTO = (List<SaleDTO>)saleService.GetSalesAsync().Result;
            string[] types = Enum.GetNames(typeof(ProductType));
            foreach (var item in types)
            {
                Sales.Add(item, SalesDTO.Where(x => x.ProductType.ToString() == item).Count());
            }         
        }
    }
}
