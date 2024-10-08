using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestMobile.Models
{
   public class Details
    {

        public required Specification Specification { get; set; }
        public OwnerShip? Ownership { get; set; }

        [JsonPropertyName("equipment")]
        public List<string>? Equipment { get; set; }
    }
}
