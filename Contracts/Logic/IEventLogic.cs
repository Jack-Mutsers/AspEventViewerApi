using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IEventLogic
    {
        IEnumerable<EventDto> GetAllEvents();
        IEnumerable<EventDto> GetAllActiveEvents();
        IEnumerable<EventDto> GetByName(string name);
        IEnumerable<EventDto> GetSortedByName(bool ascending);
        IEnumerable<EventDto> GetSortedByStartDate(bool ascending);
        EventDto GetByIdWithDetails(int event_id);
        EventDto GetById(int event_id);
        bool Create(EventForCreationDto eventForCreation);
        bool Update(EventForUpdateDto eventForUpdate);
        bool Delete(int id);
    }
}
