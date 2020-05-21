using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IEventGenreRepository
    {
        IEnumerable<EventGenre> GetByEvent(int event_id);
        IEnumerable<EventGenre> GetByEventWithDetails(int event_id);
        IEnumerable<EventGenre> GetByGenre(int genre_id);
        void DeleteEventGenre(EventGenre eventGenre);
    }
}
