using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IEventRepository
    {
        IEnumerable<Event> Get_All_Events();
        Event Get_Event_By_Id(int event_id);
        void Create_Event(Event @event);
        void Update_Event(Event @event);
        void Delete_Event(Event @event);
    }
}
