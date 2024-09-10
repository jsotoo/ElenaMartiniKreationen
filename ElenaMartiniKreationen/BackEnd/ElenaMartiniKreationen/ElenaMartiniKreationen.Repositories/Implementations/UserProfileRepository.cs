using ElenaMartiniKreationen.DataAccess.Data;
using ElenaMartiniKreationen.Entities;
using ElenaMartiniKreationen.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElenaMartiniKreationen.Repositories.Implementations
{
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ElenaMartiniKreationenDbContext context) : base(context)
        {
        }
    }
}
