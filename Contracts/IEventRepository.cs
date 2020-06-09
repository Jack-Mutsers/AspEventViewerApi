using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAllEvents();
        IEnumerable<Event> GetAllActiveEvents();
        IEnumerable<Event> GetByName(string name);
        IEnumerable<Event> GetSortedByName(bool ascending);
        IEnumerable<Event> GetSortedByStartDate(bool ascending);
        Event GetByIdWithDetails(int event_id);
        Event GetById(int event_id);
        void CreateEvent(Event @event);
        void UpdateEvent(Event @event);
        void DeleteEvent(Event @event);
    }
}
