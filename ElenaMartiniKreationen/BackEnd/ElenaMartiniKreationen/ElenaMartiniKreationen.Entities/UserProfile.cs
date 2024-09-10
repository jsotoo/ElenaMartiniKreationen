using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.Entities
{
    public class UserProfile: EntityBase
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public DateTime RegistrationDate { get; set; }

        public UserType UserType { get; set; } = default!;

        public int UserTypeId { get; set; }

        // Relación 1:N con Order
        public ICollection<Order> Orders { get; set; } = default!;

        // Relación 1:N con ShippingAddress
        public ICollection<ShippingAddress> ShippingAddresses { get; set; } = default!;

        // Relación 1:N con PaymentMethod
        public ICollection<PaymentMethod> PaymentMethods { get; set; } = default!;



    }
}
