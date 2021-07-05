using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Board.Api.Models;

namespace Board.Api.Data
{
    public interface IUsersRepository
    {
        IQueryable<User> Users { get; }
    }
}
