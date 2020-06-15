using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IGenreLogic
    {
        IEnumerable<GenreDto> GetAllGenres();
        IEnumerable<GenreDto> GetByEvent(int event_id);
        IEnumerable<GenreDto> GetByArtist(int artist_id);
        GenreDto GetById(int genre_id);
    }
}
