using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OOOShoesLib
{
    public class Products
    {
        public string Article { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Price  { get; set; }
        public string Supplier { get; set; }
        public string Manufacturer { get; set; }
        public string Category  { get; set; }
        public int Discount  { get; set; }
        public int Quantity  { get; set; }
        public string Description  { get; set; }
        public string Photo { get; set; }
    }
}