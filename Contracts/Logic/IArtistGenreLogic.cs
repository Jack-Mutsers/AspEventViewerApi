using Entities.DataTransferObjects;
using System.Collections.Generic;

namespace Contracts.Logic
{
    public interface IArtistGenreLogic
    {
        IEnumerable<ArtistGenreDto> GetByArtist(int artist_id);
        IEnumerable<ArtistGenreDto> GetByArtistWithDetails(int artist_id);
        IEnumerable<ArtistGenreDto> GetByGenre(int genre_id);
        ArtistGenreDto GetByIds(int artist_id, int genre_id);
        bool Create(ArtistGenreForCreationDto model);
        bool Delete(int event_id, int genre_id);
        bool DeleteByArtist(int id);
    }
}
