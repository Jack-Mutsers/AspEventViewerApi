using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IEventGenreRepository : IUniversalRepository<EventGenre>
    {
        EventGenre GetRecord(int event_id, int genre_id);
        IEnumerable<EventGenre> GetByEvent(int event_id);
        IEnumerable<EventGenre> GetByEventWithDetails(int event_id);
        IEnumerable<EventGenre> GetByGenre(int genre_id);
        IEnumerable<Event> GetEventsByGenre(int Genre_id);
    }
}
