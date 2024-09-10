using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.Entities
{
    public class ShippingAddress: EntityBase
    {
        public string Address { get; set; } = default!; 
        public string City { get; set; } = default!;
        public string PostalCode { get; set; } = default!;

        // Foreign Key
        public int UserId { get; set; }
        public UserProfile User { get; set; } = default!;
    }
}
