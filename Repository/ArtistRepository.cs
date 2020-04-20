using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ArtistRepository : RepositoryBase<Artist>, IArtistRepository
    {
        public ArtistRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void CreateArtist(Artist artist)
        {
            Create(artist);
        }

        public void DeleteArtist(Artist artist)
        {
            Delete(artist);
        }

        public IEnumerable<Artist> GetAllArtists()
        {
            return FindAll();
        }

        public IEnumerable<Artist> GetArtistsByGenre(int genre_id)
        {
            return FindByCondition(g => g.genre_id == genre_id);
        }

        public Artist GetById(int artist_id)
        {
            return FindByCondition(a => a.id == artist_id).FirstOrDefault();
        }

        public void UpdateArtist(Artist artist)
        {
            Update(artist);
        }
    }
}
