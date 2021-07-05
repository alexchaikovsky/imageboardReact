using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Board.Api.Models;

namespace Board.Api.Data
{
    public class EFUsersRepository : IUsersRepository
    {
        private BoardDbContext context;
        public EFUsersRepository(BoardDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<User> Users => context.Users;
    }
}
