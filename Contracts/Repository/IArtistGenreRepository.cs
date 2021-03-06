﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IArtistGenreRepository : IUniversalRepository<ArtistGenre>
    {
        IEnumerable<ArtistGenre> GetByArtist(int artist_id);
        IEnumerable<ArtistGenre> GetByArtistWithDetails(int artist_id);
        IEnumerable<ArtistGenre> GetByGenre(int genre_id);
        ArtistGenre GetRecord(int artist_id, int genre_id);
    }
}
