using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.Entities
{
    public class PaymentMethod: EntityBase
    {
        public string Type { get; set; } = default!;
        public string Token { get; set; } = default!;

        // Foreign Key
        public int UserId { get; set; }
        public UserProfile User { get; set; } = default!;
    }
}
