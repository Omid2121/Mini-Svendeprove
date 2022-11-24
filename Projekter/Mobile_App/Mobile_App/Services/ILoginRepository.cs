using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VareskanningModels.SQL;

namespace Mobile_App.Services
{
    public interface ILoginRepository
    {
        Task<User> Login(string username, string password);
        Task<User> SignUp(string username, string password);
    }
}
