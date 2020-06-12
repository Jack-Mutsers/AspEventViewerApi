using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IGenreRepository : IUniversalRepository<Genre>
    {
        IEnumerable<Genre> GetAllGenres();
        IEnumerable<Genre> GetByEvent(int event_id);
        IEnumerable<Genre> GetByArtist(int artist_id);
        Genre GetById(int genre_id);
    }
}
