using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace VareskanningModels
{
    public class PartialSale : HasId
    {
        public string Barcode { get; set; }
        public string UserId { get; set; }
    }
}
