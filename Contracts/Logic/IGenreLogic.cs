using Entities.DataTransferObjects;
using System.Collections.Generic;

namespace Contracts.Logic
{
    public interface IGenreLogic
    {
        IEnumerable<GenreDto> GetAllGenres();
        IEnumerable<GenreDto> GetByEvent(int event_id);
        IEnumerable<GenreDto> GetByArtist(int artist_id);
        GenreDto GetById(int genre_id);
    }
}
