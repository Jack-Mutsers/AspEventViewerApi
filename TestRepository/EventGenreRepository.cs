using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository
{
    public class EventGenreRepository : IEventGenreRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<EventGenre> _eventGenres;

        public EventGenreRepository() 
        {
            _eventGenres = collection.eventGenres;
        }

        public IEnumerable<EventGenre> GetByGenre(int genre_id)
        {
            return _eventGenres.Where(eg => eg.genre_id == genre_id);
        }

        public IEnumerable<EventGenre> GetByEvent(int event_id)
        {
            return _eventGenres.Where(eg => eg.event_id == event_id);
        }

        public IEnumerable<EventGenre> GetByEventWithDetails(int event_id)
        {
            List<EventGenre> eventGenres = _eventGenres.Where(eg => eg.event_id == event_id && eg.@event.active == true).ToList();

            foreach (EventGenre eventGenre in eventGenres)
            {
                eventGenre.genre = collection.genres.Where(g => g.id == eventGenre.genre_id).FirstOrDefault();
            }

            return eventGenres;
        }

        public IEnumerable<Event> GetEventsByGenre(int Genre_id)
        {
            List<EventGenre> eventGenres = _eventGenres.Where(eg => eg.genre_id == Genre_id && eg.@event.active == true).ToList();

            foreach (EventGenre eventGenre in eventGenres)
            {
                eventGenre.@event = collection.events.Where(e => e.id == eventGenre.event_id).FirstOrDefault();
                eventGenre.@event.datePlannings = collection.datePlannings.Where(dp => dp.Eventid == eventGenre.event_id).ToList();

                eventGenre.@event.genre = collection.eventGenres.Where(eg => eg.event_id == eventGenre.event_id).ToList();
                foreach (EventGenre eventGenre1 in eventGenre.@event.genre)
                {
                    eventGenre1.genre = collection.genres.Where(g => g.id == eventGenre1.genre_id).FirstOrDefault();
                }
            }

            return eventGenres.Select(eg => eg.@event);
        }

        public void DeleteEventGenre(EventGenre eventGenre)
        {
            _eventGenres.Remove(eventGenre);
        }
    }
}
