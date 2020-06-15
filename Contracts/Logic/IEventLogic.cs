using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Logic
{
    public interface IEventLogic
    {
        IEnumerable<EventDto> GetAllEvents();
        IEnumerable<EventDto> GetAllActiveEvents();
        IEnumerable<EventDto> SortEventData(OrderRequest orderRequest);
        IEnumerable<Event> GetEventByStartDate(bool ascending);
        IEnumerable<EventDto> GetByName(string name);
        IEnumerable<EventDto> GetAllByGenre(int genre_id);
        EventDto GetByIdWithDetails(int event_id);
        EventDto GetById(int event_id);
        bool Create(EventForCreationDto eventForCreation);
        bool Update(EventForUpdateDto eventForUpdate);
        bool Delete(int id);
    }
}
