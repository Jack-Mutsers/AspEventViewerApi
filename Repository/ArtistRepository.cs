using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ArtistRepository : RepositoryBase<Artist>, IArtistRepository
    {
        public ArtistRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void Create_Artist(Artist artist)
        {
            Create(artist);
        }

        public void Delete_Artist(Artist artist)
        {
            Delete(artist);
        }

        public IEnumerable<Artist> Get_All_artists()
        {
            return FindAll();
        }

        public Artist Get_Artist_By_Id(int artist_id)
        {
            return FindByCondition(a => a.id == artist_id).FirstOrDefault();
        }

        public void Update_Artist(Artist artist)
        {
            Update(artist);
        }
    }
}
