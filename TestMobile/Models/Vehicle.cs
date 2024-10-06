using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestMobile.Models
{
    public class Vehicle
    {
        [JsonPropertyName("Make")]
        public required string Make { get; set; }

        [JsonPropertyName("Model")]
        public required string Model { get; set; }

        [JsonPropertyName("Image")]
        public string? Image { get; set; }

        [JsonPropertyName("Engine Size")]
        public required string EngineSize { get; set; }

        [JsonPropertyName("Fuel Type")]
        public required string FuelType { get; set; }

        [JsonPropertyName("Year")]
        public int Year { get; set; }

        [JsonPropertyName("Mileage")]
        public int Mileage { get; set; }

        [JsonPropertyName("Auction Date and Time")]
        public DateTime AuctionDateAndTime { get; set; }

        [JsonPropertyName("Starting Bid")]
        public decimal StartingBid { get; set; }

        [JsonPropertyName("Favourite")]
        public bool Favourite { get; set; }

        [JsonPropertyName("Details")]
        public required Details Details { get; set; }

    }
}
