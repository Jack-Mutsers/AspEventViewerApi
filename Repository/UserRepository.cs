using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void Create_User(User user)
        {
            Create(user);
        }

        public void Delete_User(User user)
        {
            Delete(user);
        }

        public User Get_User_By_Login(User user)
        {
            return FindByCondition(u => u.username == user.username && u.password == user.password).FirstOrDefault();
        }

        public void Update_User(User user)
        {
            Update(user);
        }
    }
}
