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
    public class EventGenreRepository : RepositoryBase<EventGenre>, IEventGenreRepository
    {
        public EventGenreRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<EventGenre> GetByGenre(int genre_id)
        {
            return FindByCondition(eg => eg.genre_id == genre_id);
        }

        public IEnumerable<EventGenre> GetByEvent(int event_id)
        {
            return FindByCondition(eg => eg.event_id == event_id);
        }

        public IEnumerable<EventGenre> GetByEventWithDetails(int event_id)
        {
            return FindByCondition(eg => eg.event_id == event_id)
                .Include(eg => eg.genre);
        }
    }
}
