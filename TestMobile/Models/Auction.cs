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

        [JsonPropertyName("auctionDateTime")]
        public required string DateAndTimeRaw { get; set; }

        public DateTime DateTime
        {
            get
            {
                return DateTime.Parse(DateAndTimeRaw);
            }
        }

        [JsonPropertyName("Vehicles")]
        public required List<Vehicle> Vehicles { get; set; }

        public int VehiclesCount => Vehicles?.Count ?? 0;
    }
}
