using Contracts.Repository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

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

    }
}
