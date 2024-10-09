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
        [JsonPropertyName("specification")]
        public required Specification Specification { get; set; }

        [JsonPropertyName("ownership")]
        public OwnerShip? Ownership { get; set; }

        [JsonPropertyName("equipment")]
        public List<string>? Equipment { get; set; }
    }
}
