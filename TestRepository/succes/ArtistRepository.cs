using Contracts;
using Contracts.Repository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository.succes
{
    public class ArtistRepository : RepositoryBase, IArtistRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Artist> _artists;

        public ArtistRepository(RepositoryContext repositoryContext = null) : base(repositoryContext)
        {
            _artists = collection.artists;
        }

        public IEnumerable<Artist> GetAllArtists()
        {
            return _artists;
        }

        public IEnumerable<Artist> GetArtistsByEventDate(int event_date_id)
        {
            return _artists.Where(a => a.ScheduleItem.schedule.stage.event_date_id == event_date_id);
        }

        public Artist GetById(int artist_id)
        {
            return _artists.Where(a => a.id == artist_id).FirstOrDefault();
        }

        public Artist GetByIdWithDetails(int artist_id)
        {
            Artist artist = _artists.Where(a => a.id == artist_id).FirstOrDefault();
            artist.genre = collection.artistGenres.Where(ag => ag.artist_id == artist.id).ToList();
            return artist;
        }

        public void Create(Artist model)
        {
            model.id = (_artists.Count() + 1);
            _artists.Add(model);
        }

        public void Update(Artist model)
        {
            Artist obj = _artists.FirstOrDefault(a => a.id == model.id);
            if (obj != null)
                obj = model;

            var test = 1;
        }

        public void Delete(Artist model)
        {
            _artists.Remove(model);
        }

    }
}
