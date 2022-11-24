using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace VareskanningModels
{
    public class Login : HasId
    {
        /// <summary>
        /// Username of the person who scanned the product.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Password of the person who scanned the product.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
