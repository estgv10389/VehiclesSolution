using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMobile.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public byte[]? Image { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string? User { get; set; }
    }
}
