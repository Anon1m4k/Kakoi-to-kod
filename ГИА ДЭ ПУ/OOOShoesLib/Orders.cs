using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOOShoesLib
{
    public class Orders
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int PickupPointId { get; set; }
        public int ClientId { get; set; }
        public int PickupCode { get; set; }
        public string Status { get; set; }
        public string ClientName { get; set; }
        public string PickupAddress { get; set; }
    }
}
