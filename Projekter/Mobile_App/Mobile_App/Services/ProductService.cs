using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VareskanningModels.SQL;

namespace Mobile_App.Services
{
    public class ProductService : BaseService<Product>
    {
        public override async Task<IEnumerable<Product>> GetItemsAsync()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var products = new List<Product>();
                var client = new HttpClient();
                HttpResponseMessage response =  client.GetAsync(PRODUCT_URL).Result;

                if (response.IsSuccessStatusCode)
                {
                    products = await response.Content.ReadFromJsonAsync<List<Product>>();
                }
                return await Task.FromResult(products);
            }
            return null;
        }

        public override async Task<bool> AddItemAsync(Product product)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                string json = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsync(PRODUCT_URL, content).Result;

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
                HttpResponseMessage response = await client.DeleteAsync(PRODUCT_URL + "/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);
                }
            }
            return false;
        }
    }
}
