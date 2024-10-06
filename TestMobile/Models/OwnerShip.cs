using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestMobile.Models
{
    public class OwnerShip
    {
        [JsonPropertyName("Logbook")]
        public bool Logbook { get; set; }

        [JsonPropertyName("Number Of Owners")]
        public int NumberOfOwners { get; set; }

        [JsonPropertyName("Date Of Registration")]
        public DateTime DateOfRegistration { get; set; }
    }
}
