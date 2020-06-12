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
    public class ArtistGenreRepository : RepositoryBase, IArtistGenreRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<ArtistGenre> _artistGenres;


        public ArtistGenreRepository(RepositoryContext repositoryContext = null) :base(repositoryContext)
        {
            _artistGenres = collection.artistGenres;
        }

        public IEnumerable<ArtistGenre> GetByArtist(int artist_id)
        {
            return _artistGenres.Where(eg => eg.artist_id == artist_id);
        }

        public IEnumerable<ArtistGenre> GetByArtistWithDetails(int artist_id)
        {
            List<ArtistGenre> artistGenres = _artistGenres.Where(eg => eg.artist_id == artist_id).ToList();

            foreach (ArtistGenre artistGenre in artistGenres)
            {
                artistGenre.genre = collection.genres.Where(g => g.id == artistGenre.genre_id).FirstOrDefault();
            }

            return artistGenres;
        }

        public IEnumerable<ArtistGenre> GetByGenre(int genre_id)
        {
            return _artistGenres.Where(eg => eg.genre_id == genre_id);
        }

        public void Create(ArtistGenre model)
        {
            throw new NotImplementedException();
        }

        public void Update(ArtistGenre model)
        {
            throw new NotImplementedException();
        }

        public void Delete(ArtistGenre model)
        {
            throw new NotImplementedException();
        }
    }
}
