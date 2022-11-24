using System.Net.Http.Json;
using VareskanningModels.SQL;

namespace Mobile_App.Services
{
    public class LoginService : ILoginRepository
    {
        public async Task<User> Login(string username, string password)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var user = new User();
                    var client = new HttpClient();
                    string url = "http://10.161.57.152:45455/api/user/login";
                    //client.BaseAddress = new Uri(url);
                    HttpResponseMessage response = await client.PostAsJsonAsync(url, new User { Username = username, Password = password });

                    if (response.IsSuccessStatusCode)
                    {
                        user = await response.Content.ReadFromJsonAsync<User>();
                        return await Task.FromResult(user);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> SignUp(string username, string password)
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var user = new User();
                    var client = new HttpClient();
                    string url = "http://192.168.1.100:45455/api/user";
                    //client.BaseAddress = new Uri(url);
                    HttpResponseMessage response = await client.PostAsJsonAsync(url, new User { Username = username, Password = password });

                    if (response.IsSuccessStatusCode)
                    {
                        user = await response.Content.ReadFromJsonAsync<User>();
                        return await Task.FromResult(user);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
