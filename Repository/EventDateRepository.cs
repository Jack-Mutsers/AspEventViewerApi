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
    public class EventDateRepository : RepositoryBase<EventDate>, IEventDateRepository
    {
        public EventDateRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public EventDate GetById(int event_date_id)
        {
            return FindByCondition(ed => ed.id == event_date_id)
                .FirstOrDefault();
        }

        public EventDate GetByIdWithDetails(int event_date_id)
        {
            return FindByCondition(ed => ed.id == event_date_id)
                .Include(ed => ed.DatePlanning).ThenInclude(dp => dp.@event).ThenInclude(e => e.genre).ThenInclude(g => g.genre)
                .Include(ed => ed.stages).ThenInclude(s => s.schedule).ThenInclude(sc => sc.scheduleItems).ThenInclude(si => si.artist)
                .Include(ed => ed.reviews.Where(r => r.validated == true)).ThenInclude(r => r.user)
                .FirstOrDefault();
        }

    }
}
