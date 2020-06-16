using Contracts;
using Contracts.Repository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository.succes
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<User> _users;

        public UserRepository(RepositoryContext repositoryContext = null) : base(repositoryContext)
        {
            _users = collection.users;
        }

        public void Create(User user)
        {
            user.id = (_users.Count() + 1);
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public User GetById(int User_id)
        {
            User user = _users.Where(u => u.id == User_id).FirstOrDefault();

            if (user != null)
            {
                user.right = collection.rights.Where(r => r.id == user.right_id).FirstOrDefault();
                user.preference = collection.preferences.Where(p => p.user_id == user.id).ToList();

                foreach (Preference preference in user.preference)
                {
                    preference.genre = collection.genres.Where(g => g.id == preference.genre_id).FirstOrDefault();
                }
            }

            return user;
        }

        public User GetUserByLogin(string username, string password)
        {
            User user = _users.Where(u => u.username == username).FirstOrDefault();

            if (user != null)
            {
                user.right = collection.rights.Where(r => r.id == user.right_id).FirstOrDefault();
                user.preference = collection.preferences.Where(p => p.user_id == user.id).ToList();

                foreach (Preference preference in user.preference)
                {
                    preference.genre = collection.genres.Where(g => g.id == preference.genre_id).FirstOrDefault();
                }
            }

            return user;
        }

        public void Update(User user)
        {
            User user1 = _users.FirstOrDefault(u => u.id == user.id);
            if (user1 != null)
                user1 = user;
        }

    }
}
