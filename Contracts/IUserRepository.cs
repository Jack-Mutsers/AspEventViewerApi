using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IUserRepository
    {
        User Get_User_By_Login(User user);
        void Create_User(User user);
        void Update_User(User user);
        void Delete_User(User user);
    }
}
