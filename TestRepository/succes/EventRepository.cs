using Contracts;
using Contracts.Repository;
using Entities;
using Entities.Models;
using Logics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository.succes
{
    public class EventRepository : RepositoryBase, IEventRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Event> _events;

        public EventRepository(RepositoryContext repositoryContext = null) : base(repositoryContext)
        {
            _events = collection.events;
        }

        public IEnumerable<Event> GetAllActiveEvents()
        {
            List<Event> events = _events.Where(e => e.active == true).ToList();

            foreach (Event @event in events)
            {
                @event.datePlannings = collection.datePlannings.Where(dp => dp.Eventid == @event.id).ToList();
                @event.genre = collection.eventGenres.Where(eg => eg.event_id == @event.id).ToList();

                foreach (EventGenre eventGenre in @event.genre)
                {
                    eventGenre.genre = collection.genres.Where(g => g.id == eventGenre.genre_id).FirstOrDefault();
                }
            }

            return events;
        }

        public IEnumerable<Event> GetAllEvents()
        {
            List<Event> events = _events;

            foreach (Event @event in events)
            {
                @event.datePlannings = collection.datePlannings.Where(dp => dp.Eventid == @event.id).ToList();
                @event.genre = collection.eventGenres.Where(eg => eg.event_id == @event.id).ToList();

                foreach (EventGenre eventGenre in @event.genre)
                {
                    eventGenre.genre = collection.genres.Where(g => g.id == eventGenre.genre_id).FirstOrDefault();
                }
            }

            return events;
        }

        public Event GetById(int event_id)
        {
            return _events.Where(e => e.id == event_id).FirstOrDefault();
        }

        public Event GetByIdWithDetails(int event_id)
        {
            Event @event = _events.Where(e => e.id == event_id).FirstOrDefault();

            @event.genre = collection.eventGenres.Where(eg => eg.event_id == @event.id).ToList();

            foreach (EventGenre eventGenre in @event.genre)
            {
                eventGenre.genre = collection.genres.Where(g => g.id == eventGenre.genre_id).FirstOrDefault();
            }

            return @event;
        }

        public IEnumerable<Event> GetByName(string name)
        {
            List<Event> events = _events.Where(e => e.active == true && e.name.IndexOf(name, StringComparison.OrdinalIgnoreCase) != -1).ToList();

            foreach (Event @event in events)
            {
                @event.datePlannings = collection.datePlannings.Where(dp => dp.Eventid == @event.id).ToList();
                @event.genre = collection.eventGenres.Where(eg => eg.event_id == @event.id).ToList();

                foreach (EventGenre eventGenre in @event.genre)
                {
                    eventGenre.genre = collection.genres.Where(g => g.id == eventGenre.genre_id).FirstOrDefault();
                }
            }

            return events;
        }

        public void Create(Event @event)
        {
            _events.Add(@event);
        }

        public void Delete(Event @event)
        {
            _events.Remove(@event);
        }

        public void Update(Event @event)
        {
            Event @event2 = _events.FirstOrDefault(e => e.id == @event.id);
            if (@event2 != null)
                @event2 = @event;
        }

        public IEnumerable<Event> GetSortedByName(bool ascending)
        {
            List<Event> events;
            if (ascending)
                events = _events.OrderBy(e => e.name).ToList();
            else
                events = _events.OrderByDescending(e => e.name).ToList();

            foreach (Event @event in events)
            {
                @event.genre = collection.eventGenres.Where(eg => eg.event_id == @event.id).ToList();

                foreach (EventGenre eventGenre in @event.genre)
                {
                    eventGenre.genre = collection.genres.Where(g => g.id == eventGenre.genre_id).FirstOrDefault();
                }
            }

            return events;
        }

        public IEnumerable<Event> GetSortedByStartDate(bool ascending)
        {
            List<Event> events = GetAllActiveEvents().ToList();

            EventSorter sorter = new EventSorter();
            return sorter.OrderByStartDate(events, ascending);
        }

    }
}