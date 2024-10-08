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
        [JsonPropertyName("logBook")]
        public required string Logbook { get; set; }

        [JsonPropertyName("numberOfOwners")]
        public int NumberOfOwners { get; set; }

        [JsonPropertyName("dateOfRegistration")]
        public required string DateOfRegistration { get; set; }

        public DateTime DateTime
        {
            get
            {
                return DateTime.Parse(DateOfRegistration);
            }
        }
    }
}
