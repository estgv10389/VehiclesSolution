using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestMobile.Models
{
   public class Auction
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("DateTime")]
        public DateTime DateTime { get; set; }

        [JsonPropertyName("Vehicles")]
        public required List<Vehicle> Vehicles { get; set; }
    }
}
