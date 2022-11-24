using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace VareskanningModels.SQL
{
    public class User : Login
    {
        //[InverseProperty("User")]
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
