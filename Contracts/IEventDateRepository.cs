using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IEventDateRepository
    {
        EventDate Get_Event_Date_By_Id(int event_date_id);
        void Create_Event_Date(EventDate event_date);
        void Update_Event_Date(EventDate event_date);
        void Delete_Event_Date(EventDate event_date);
    }
}
