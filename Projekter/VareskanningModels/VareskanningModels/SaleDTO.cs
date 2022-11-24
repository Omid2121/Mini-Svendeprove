using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace VareskanningModels
{
    public class SaleDTO : HasId
    {
        public DateTimeOffset Timestamp { get; set; }
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public ProductType ProductType { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
    }
}
