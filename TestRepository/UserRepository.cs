using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository
{
    public class UserRepository : IUserRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<User> _users;

        public UserRepository() 
        {
            _users = collection.users;
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public User GetById(int User_id)
        {
            return FindByCondition(u => u.id == User_id)
                .Include(u => u.right)
                .Include(u => u.preference).ThenInclude(p => p.genre)
                .FirstOrDefault();
        }

        public User GetUserByLogin(string username, string password)
        {
            return FindByCondition(u => u.username == username /*&& u.password == password*/)
                .Include(u => u.right)
                .Include(u => u.preference).ThenInclude(p => p.genre)
                .FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            User user1 = _users.FirstOrDefault(u => u.id == user.id);
            if (user1 != null)
                user1 = user;
        }
    }
}
