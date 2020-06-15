using Contracts;
using Contracts.Repository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository
{
    public class GenreRepository : RepositoryBase, IGenreRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Genre> _genres;

        public GenreRepository(RepositoryContext repositoryContext = null) : base(repositoryContext)
        {
            _genres = collection.genres;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _genres;
        }

        public IEnumerable<Genre> GetByArtist(int artist_id)
        {
            List<Genre> genres = _genres;

            foreach (Genre genre in genres)
            {
                genre.artistGenre = collection.artistGenres.Where(ag => ag.artist_id == artist_id && ag.genre_id == genre.id).FirstOrDefault();
            }

            return genres.Where(e => e.eventGenre.event_id == artist_id);
        }

        public IEnumerable<Genre> GetByEvent(int event_id)
        {
            List<Genre> genres = _genres;

            foreach (Genre genre in genres)
            {
                genre.eventGenre = collection.eventGenres.Where(eg => eg.event_id == event_id && eg.genre_id == genre.id).FirstOrDefault();
            }

            return genres.Where(e => e.eventGenre.event_id == event_id);
        }

        public Genre GetById(int genre_id)
        {
            return _genres.Where(g => g.id == genre_id).FirstOrDefault();
        }

        public void Create(Genre model)
        {
            //throw new NotImplementedException();
        }

        public void Update(Genre model)
        {
            //throw new NotImplementedException();
        }

        public void Delete(Genre model)
        {
            //throw new NotImplementedException();
        }

    }
}
