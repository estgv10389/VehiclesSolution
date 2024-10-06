using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestMobile.Models
{
   public class Specification
    {
        [JsonPropertyName("Vehicle Type")]
        public required string VehicleType { get; set; }

        [JsonPropertyName("Colour")]
        public required string Colour { get; set; }

        [JsonPropertyName("Fuel")]
        public required string Fuel { get; set; }
        [JsonPropertyName("Transmission")]
        public required string Transmission { get; set; }

        [JsonPropertyName("Number Of Doors")]
        public int NumberOfDoors { get; set; }

        [JsonPropertyName("CO2 Emissions")]
        public decimal CO2Emissions { get; set; }

        [JsonPropertyName("NOX Emissions")]
        public decimal NOXEmissions { get; set; }

        [JsonPropertyName("Number Of Keys")]
        public int NumberOfKeys { get; set; }
    }
}
