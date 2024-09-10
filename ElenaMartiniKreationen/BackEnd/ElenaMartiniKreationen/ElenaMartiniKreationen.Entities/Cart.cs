using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.Entities
{
    public class Cart : EntityBase
    {
        //Foreing Key

        public int UserId { get; set; }
        public UserProfile User { get; set; } = default!;
       

        public DateTime CreationDate { get; set; }
    }
}
