using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.Entities
{
    public class Order : EntityBase
    {
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = default!;

        // Foreign Key
        public int UserId { get; set; }
        public UserProfile User { get; set; } = default!;

        // Relación 1:N con OrderItem
        public ICollection<OrderItem> OrderItems { get; set; } = default!;

    }
}
