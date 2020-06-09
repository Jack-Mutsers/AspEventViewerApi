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
    public class GenreRepository : IGenreRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Genre> _genres;

        public GenreRepository() 
        {
            _genres = collection.genres;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return FindAll();
        }

        public IEnumerable<Genre> GetByArtist(int artist_id)
        {
            return FindByCondition(e => e.artistGenre.artist_id == artist_id)
                .Include(e => e.artistGenre);
        }

        public IEnumerable<Genre> GetByEvent(int event_id)
        {
            return FindByCondition(e => e.eventGenre.event_id == event_id)
                .Include(e => e.eventGenre);
        }

        public Genre GetById(int genre_id)
        {
            return FindByCondition(g => g.id == genre_id).FirstOrDefault();
        }
    }
}
