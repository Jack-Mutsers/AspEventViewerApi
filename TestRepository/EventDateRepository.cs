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
    public class EventDateRepository : RepositoryBase, IEventDateRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<EventDate> _eventDates;

        public EventDateRepository(RepositoryContext repositoryContext = null) : base(repositoryContext)
        {
            _eventDates = collection.eventDates;
        }

        public void Create(EventDate event_date)
        {
            _eventDates.Add(event_date);
        }

        public void Delete(EventDate event_date)
        {
            _eventDates.Remove(event_date);
        }

        public EventDate GetById(int event_date_id)
        {
            return _eventDates.Where(ed => ed.id == event_date_id)
                .FirstOrDefault();
        }

        public EventDate GetByIdWithDetails(int event_date_id)
        {
            EventDate eventDate = _eventDates.Where(ed => ed.id == event_date_id)
                .FirstOrDefault();

            eventDate.DatePlanning = collection.datePlannings.Where(dp => dp.id == eventDate.planning_id).FirstOrDefault();
            eventDate.DatePlanning.@event = collection.events.Where(e => e.id == eventDate.DatePlanning.Eventid).FirstOrDefault();
            eventDate.DatePlanning.@event.genre = collection.eventGenres.Where(eg => eg.event_id == eventDate.DatePlanning.@event.id).ToList();

            foreach (EventGenre eventGenre in eventDate.DatePlanning.@event.genre)
            {
                eventGenre.genre = collection.genres.Where(g => g.id == eventGenre.genre_id).FirstOrDefault();
            }

            eventDate.stages = collection.stages.Where(s => s.event_date_id == eventDate.id).ToList();

            foreach (Stage stage in eventDate.stages)
            {
                stage.schedule = collection.schedules.Where(s => s.stage_id == stage.id).ToList();

                foreach (Schedule schedule in stage.schedule)
                {
                    schedule.scheduleItems = collection.scheduleItems.Where(si => si.schedule_id == schedule.id).ToList();

                    foreach (ScheduleItem scheduleItem in schedule.scheduleItems)
                    {
                        scheduleItem.artist = collection.artists.Where(a => a.id == scheduleItem.artist_id).First();
                    }
                }
            }

            eventDate.reviews = collection.reviews.Where(r => r.event_date_id == eventDate.id && r.validated == true).ToList();

            foreach (Review review in eventDate.reviews)
            {
                review.user = collection.users.Where(u => u.id == review.user_id).FirstOrDefault();
            }

            return eventDate;
        }

        public void Update(EventDate event_date)
        {
            EventDate eventDate = _eventDates.FirstOrDefault(ed => ed.id == event_date.id);
            if (eventDate != null)
                eventDate = event_date;
        }

    }
}
