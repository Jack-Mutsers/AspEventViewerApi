using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ArtistGenreRepository : RepositoryBase<ArtistGenre>, IArtistGenreRepository
    {
        public ArtistGenreRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<ArtistGenre> GetByArtist(int artist_id)
        {
            return FindByCondition(eg => eg.artist_id == artist_id);
        }

        public IEnumerable<ArtistGenre> GetByArtistWithDetails(int artist_id)
        {
            return FindByCondition(eg => eg.artist_id == artist_id)
                .Include(eg => eg.genre);
        }

        public IEnumerable<ArtistGenre> GetByGenre(int genre_id)
        {
            return FindByCondition(eg => eg.genre_id == genre_id);
        }
    }
}
