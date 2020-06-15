using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
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
