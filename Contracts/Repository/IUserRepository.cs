using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IUserRepository : IUniversalRepository<User>
    {
        User GetUserByLogin(string username, string password);
        User GetById(int User_id);
    }
}
