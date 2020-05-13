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
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void CreateEvent(Event @event)
        {
            Create(@event);
        }

        public void DeleteEvent(Event @event)
        {
            Delete(@event);
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return FindAll().Include(e => e.genre).ThenInclude(g => g.genre);
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

        public void UpdateEvent(Event @event)
        {
            Update(@event);
        }
    }
}