using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace VareskanningModels.SQL
{
    public class Product : HasId
    {
        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product's barcode.
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// Type of product.
        /// </summary>
        public ProductType ProductType { get; set; }

        /// <summary>
        /// Price of product.
        /// </summary>
        public decimal Price { get; set; }

        [NotMapped]
        public string SelectedType { get => ProductType.ToString();}

        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
