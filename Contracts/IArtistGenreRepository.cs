using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IArtistGenreRepository
    {
        IEnumerable<ArtistGenre> GetByArtist(int artist_id);
        IEnumerable<ArtistGenre> GetByArtistWithDetails(int artist_id);
        IEnumerable<ArtistGenre> GetByGenre(int genre_id);
    }
}
