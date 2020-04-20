using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IEventDateRepository
    {
        EventDate GetById(int event_date_id);
        void CreateEventDate(EventDate event_date);
        void UpdateEventDate(EventDate event_date);
        void DeleteEventDate(EventDate event_date);
    }
}
