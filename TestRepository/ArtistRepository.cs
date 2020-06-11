using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository
{
    public class ArtistRepository : IArtistRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Artist> _artists;

        public ArtistRepository() 
        {
            _artists = collection.artists;
        }

        public void CreateArtist(Artist artist)
        {
            artist.id = (_artists.Count() + 1);
            _artists.Add(artist);
        }

        public void DeleteArtist(Artist artist)
        {
            _artists.Remove(artist);
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

        public void UpdateArtist(Artist artist)
        {
            Artist obj = _artists.FirstOrDefault(a => a.id == artist.id);
            if (obj != null) 
                obj = artist;

            var test = 1;
        }

        public Artist GetByIdWithDetails(int artist_id)
        {
            Artist artist = _artists.Where(a => a.id == artist_id).FirstOrDefault();
            artist.genre = collection.artistGenres.Where(ag => ag.artist_id == artist.id).ToList();
            return artist;
        }
    }
}
