using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Genre> Get_All_Genres()
        {
            return FindAll();
        }

        public Genre Get_Genre_By_Id(int genre_id)
        {
            return FindByCondition(g => g.id == genre_id).FirstOrDefault();
        }
    }
}
