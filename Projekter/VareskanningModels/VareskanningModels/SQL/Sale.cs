using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
#nullable disable

namespace VareskanningModels.SQL
{
    public class Sale : HasId
    {
        /// <summary>
        /// Universal Time of scan(UTC).
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
