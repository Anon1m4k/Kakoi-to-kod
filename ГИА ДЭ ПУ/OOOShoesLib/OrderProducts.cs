using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOShoesLib
{
    public class OrderProducts
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Article { get; set; }
        public int Quantity { get; set; }
    }
}

