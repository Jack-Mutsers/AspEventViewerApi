using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IArtistRepository
    {
        IEnumerable<Artist> GetAllArtists();
        //IEnumerable<Artist> GetArtistsByGenre(int genre_id);
        IEnumerable<Artist> GetArtistsByEventDate(int event_date_id);
        Artist GetById(int artist_id);
        Artist GetByIdWithDetails(int artist_id);
        void CreateArtist(Artist artist);
        void UpdateArtist(Artist artist);
        void DeleteArtist(Artist artist);
    }
}
