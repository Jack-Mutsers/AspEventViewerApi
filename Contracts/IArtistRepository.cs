using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IArtistRepository : IUniversalRepository<Artist>
    {
        IEnumerable<Artist> GetAllArtists();
        IEnumerable<Artist> GetArtistsByEventDate(int event_date_id);
        Artist GetById(int artist_id);
        Artist GetByIdWithDetails(int artist_id);
    }
}
