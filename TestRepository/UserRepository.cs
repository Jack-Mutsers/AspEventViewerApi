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
            _users.Add(user);
        }

        public void DeleteUser(User user)
        {
            _users.Remove(user);
        }

        public User GetById(int User_id)
        {
            User user = _users.Where(u => u.id == User_id).FirstOrDefault();

            user.right = collection.rights.Where(r => r.id == user.right_id).FirstOrDefault();
            user.preference = collection.preferences.Where(p => p.user_id == user.id).ToList();

            foreach (Preference preference in user.preference)
            {
                preference.genre = collection.genres.Where(g=>g.id == preference.genre_id).FirstOrDefault();
            }

            return user;
        }

        public User GetUserByLogin(string username, string password)
        {
            User user = _users.Where(u => u.username == username).FirstOrDefault();

            user.right = collection.rights.Where(r => r.id == user.right_id).FirstOrDefault();
            user.preference = collection.preferences.Where(p => p.user_id == user.id).ToList();

            foreach (Preference preference in user.preference)
            {
                preference.genre = collection.genres.Where(g => g.id == preference.genre_id).FirstOrDefault();
            }

            return user;
        }

        public void UpdateUser(User user)
        {
            User user1 = _users.FirstOrDefault(u => u.id == user.id);
            if (user1 != null)
                user1 = user;
        }
    }
}
