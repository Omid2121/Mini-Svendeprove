using RandomStringCreator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace VareskanningModels
{
    public abstract class HasId
    {
        public string Id { get; set; } = new StringCreator().Get(10);
    }
}
