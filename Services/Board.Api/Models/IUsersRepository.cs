using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Board.Api.Models
{
    public interface IUsersRepository
    {
        IQueryable<User> Users { get; }
    }
}
