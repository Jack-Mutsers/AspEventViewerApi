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

namespace TestRepository.Error
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext = null) : base(repositoryContext) { }

        public void Create(User user)
        {
            throw new Exception("this is a Unit Test for creation");
        }

        public void Delete(User user)
        {
            throw new Exception("this is a Unit Test for delete");
        }

        public User GetById(int User_id)
        {
            throw new Exception("this is a Unit Test for getbyId");
        }

        public User GetUserByLogin(string username, string password)
        {
            throw new Exception("this is a Unit Test for GetUserByLogin");
        }

        public void Update(User user)
        {
            throw new Exception("this is a Unit Test for Upate");
        }

    }
}
