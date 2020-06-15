using Contracts.Repository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Event> GetAllActiveEvents()
        {
            return FindByCondition(e => e.active == true)
                .Include(e => e.genre).ThenInclude(g => g.genre)
                .Include(e => e.datePlannings);
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return FindAll()
                .Include(e => e.genre).ThenInclude(g => g.genre)
                .Include(e => e.datePlannings);
        }

        public Event GetById(int event_id)
        {
            return FindByCondition(e => e.id == event_id).FirstOrDefault();
        }

        public Event GetByIdWithDetails(int event_id)
        {
            return FindByCondition(e => e.id == event_id)
                //.Include(e => e.genre).ThenInclude(g => g.genre)
                .FirstOrDefault();
        }

        public IEnumerable<Event> GetByName(string name)
        {
            return FindByCondition(e => e.active == true && e.name.IndexOf(name, StringComparison.OrdinalIgnoreCase) != -1)
                .Include(e => e.genre).ThenInclude(g => g.genre)
                .Include(e => e.datePlannings);
        }

        public IEnumerable<Event> GetSortedByName(bool ascending)
        {
            if(ascending)
                return FindAll().OrderBy(e => e.name).Include(e => e.genre).ThenInclude(eg => eg.genre);
            else
                return FindAll().OrderByDescending(e => e.name).Include(e => e.genre).ThenInclude(eg => eg.genre);
        }

        public IEnumerable<Event> GetSortedByStartDate(bool ascending)
        {
            return FindByRawQuery("CALL GetAllEventsOrderedByStart(" + ascending + ");");
        }
    }
}
//"FromSqlRaw or FromSqlInterpolated was called with non-composable SQL and with a query composing over it. 
// Consider calling `AsEnumerable` after the FromSqlRaw or FromSqlInterpolated method to perform the composition on the client side."