using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void Create_Event(Event @event)
        {
            Create(@event);
        }

        public void Delete_Event(Event @event)
        {
            Delete(@event);
        }

        public IEnumerable<Event> Get_All_Events()
        {
            return FindAll();
        }

        public Event Get_Event_By_Id(int event_id)
        {
            return FindByCondition(e => e.id == event_id).FirstOrDefault();
        }

        public void Update_Event(Event @event)
        {
            Update(@event);
        }
    }
}