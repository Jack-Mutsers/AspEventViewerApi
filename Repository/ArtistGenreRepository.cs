using Contracts.Repository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        public ArtistGenre GetRecord(int artist_id, int genre_id)
        {
            return FindByCondition(ag => ag.artist_id == artist_id && ag.genre_id == genre_id).FirstOrDefault();
        }
    }
}
