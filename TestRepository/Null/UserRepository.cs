using Contracts;
using Contracts.Repository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestRepository.succes;

namespace TestRepository.Null
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext = null) : base(repositoryContext) { }

        public void Create(User user){}

        public void Delete(User user){}

        public User GetById(int User_id)
        {
            return null;
        }

        public User GetUserByLogin(string username, string password)
        {
            return null;
        }

        public void Update(User user) { }

    }
}
