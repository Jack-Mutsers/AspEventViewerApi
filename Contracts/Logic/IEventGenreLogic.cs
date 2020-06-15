using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IEventGenreLogic
    {
        IEnumerable<EventGenreDto> GetByEvent(int event_id);
        IEnumerable<EventGenreDto> GetByEventWithDetails(int event_id);
        IEnumerable<EventGenreDto> GetByGenre(int genre_id);
        IEnumerable<EventDto> GetEventsByGenre(int genre_id);
        EventGenreDto GetByIds(int event_id, int genre_id);
        bool Create(EventGenreForCreationDto eventGenre);
        bool Delete(int event_id, int genre_id);
        bool DeleteByEvent(int id);
    }
}
