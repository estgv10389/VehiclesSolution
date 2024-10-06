using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMobile.Models.Components
{
   public class CustomColumnDefinition
    {
        public required string Header { get; set; }
        public required string BindingProperty { get; set; }
        public string? FormatString { get; set; } 
        public double? Width { get; set; } 
        public bool IsBold { get; set; }
    }
}
