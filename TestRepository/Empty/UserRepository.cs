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

namespace TestRepository.Empty
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext = null) : base(repositoryContext) { }

        public void Create(User user){}

        public void Delete(User user){}

        public User GetById(int User_id)
        {
            return new User();
        }

        public User GetUserByLogin(string username, string password)
        {
            return new User();
        }

        public void Update(User user) { }

    }
}
