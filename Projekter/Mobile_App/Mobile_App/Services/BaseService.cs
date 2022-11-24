using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VareskanningModels.SQL;

namespace Mobile_App.Services
{
    public abstract class BaseService<T>
    {
        internal static readonly string PRODUCT_URL = "http://10.161.57.152:45455/api/Product";
        internal static readonly string SALE_URL = "http://10.161.57.152:45455/api/Sale";

        public virtual Task<IEnumerable<T>> GetItemsAsync() => throw new NotImplementedException();
        //public virtual Task<T> GetItemAsync(string id) => throw new NotImplementedException();
        public virtual Task<bool> AddItemAsync(T item) => throw new NotImplementedException();
        //public virtual Task<T> UpdateItemAsync(string id, T item) => throw new NotImplementedException();
        public virtual Task<bool> DeleteItemAsync(string id) => throw new NotImplementedException();
    }
}
