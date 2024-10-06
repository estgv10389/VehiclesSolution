using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMobile.Models
{
   public class Details
    {
        public required Specification Specification { get; set; }
        public OwnerShip? Ownership { get; set; }
        public List<string>? Equipment { get; set; }
    }
}
