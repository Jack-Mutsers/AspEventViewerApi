using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository
{
    public class EventRepository : IEventRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Event> _events;

        public EventRepository()
        {
            _events = collection.events;
        }

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

        public void CreateEvent(Event @event)
        {
            Create(@event);
        }

        public void DeleteEvent(Event @event)
        {
            Delete(@event);
        }

        public void UpdateEvent(Event @event)
        {
            Event @event2 = _events.FirstOrDefault(e => e.id == @event.id);
            if (@event2 != null)
                @event2 = @event;
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