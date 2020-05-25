using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAllEvents();
        IEnumerable<Event> GetByName(string name);
        Event GetByIdWithDetails(int event_id);
        Event GetById(int event_id);
        void CreateEvent(Event @event);
        void UpdateEvent(Event @event);
        void DeleteEvent(Event @event);
    }
}
