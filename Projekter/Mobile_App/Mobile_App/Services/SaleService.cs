using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VareskanningModels;
using VareskanningModels.SQL;

namespace Mobile_App.Services
{
    public class SaleService : BaseService<Sale>
    {
        public async Task<IEnumerable<SaleDTO>> GetSalesAsync()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var salesDTO = new List<SaleDTO>();
                var client = new HttpClient();
                HttpResponseMessage response = client.GetAsync(SALE_URL).Result;
                
                if (response.IsSuccessStatusCode)
                {
                    salesDTO = await response.Content.ReadFromJsonAsync<List<SaleDTO>>();
                }
                return await Task.FromResult(salesDTO);
            }
            return null;
        }
        
        public async Task<bool> AddItemAsync(PartialSale partialSale)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                string json = JsonConvert.SerializeObject(partialSale);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new();
                HttpResponseMessage response = await client.PostAsync(SALE_URL, content);
                
                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);
                }
            }
            return false;
        }
       
        public override async Task<bool> DeleteItemAsync(string id)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var client = new HttpClient();
                HttpResponseMessage response = await client.DeleteAsync(SALE_URL + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);
                }
            }
            return false;
        }
    }
}
