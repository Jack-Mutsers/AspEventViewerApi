using Contracts;
using Contracts.Repository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository
{
    public class DatePlanningRepository : RepositoryBase, IDatePlanningRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<DatePlanning> _datePlannings;

        public DatePlanningRepository(RepositoryContext repositoryContext = null) : base(repositoryContext)
        {
            _datePlannings = collection.datePlannings;
        }

        public void Create(DatePlanning date_planning)
        {
            _datePlannings.Add(date_planning);
        }

        public void Delete(DatePlanning date_planning)
        {
            _datePlannings.Remove(date_planning);
        }

        public IEnumerable<DatePlanning> GetAll()
        {
            List<DatePlanning> datePlannings = _datePlannings;

            foreach (DatePlanning datePlanning in datePlannings)
            {
                datePlanning.@event = collection.events.Where(e => e.id == datePlanning.Eventid).First();
                datePlanning.event_date = collection.eventDates.Where(e => e.planning_id == datePlanning.id).First();
            }

            return datePlannings;
        }

        public IEnumerable<DatePlanning> GetAllByEvent(int event_id)
        {
            List<DatePlanning> datePlannings = _datePlannings.Where(dp => dp.Eventid == event_id).ToList();

            foreach (DatePlanning datePlanning in datePlannings)
            {
                datePlanning.event_date = collection.eventDates.Where(ed => ed.planning_id == datePlanning.id).First();
            }

            return datePlannings;
        }

        public DatePlanning GetById(int planning_id)
        {
            DatePlanning datePlanning = _datePlannings.Where(dp => dp.id == planning_id).FirstOrDefault();
            datePlanning.event_date = collection.eventDates.Where(ed => ed.planning_id == datePlanning.id).First();

            return datePlanning;
        }

        public DatePlanning GetByIdWithDetails(int planning_id)
        {
            DatePlanning datePlanning = _datePlannings.Where(dp => dp.id == planning_id).FirstOrDefault();
            datePlanning.@event = collection.events.Where(e => e.id == datePlanning.Eventid).First();
            datePlanning.event_date = collection.eventDates.Where(ed => ed.planning_id == datePlanning.id).First();

            return datePlanning;
        }

        public IEnumerable<DatePlanning> GetFinishedEventDates(int event_id)
        {
            List<DatePlanning> datePlannings = _datePlannings.Where(dp => dp.Eventid == event_id && dp.start < new DateTime(2020, 6, 9)).OrderByDescending(dp => dp.start).ToList();

            foreach (DatePlanning datePlanning in datePlannings)
            {
                datePlanning.event_date = collection.eventDates.Where(ed => ed.planning_id == datePlanning.id).First();
            }

            return datePlannings;
        }

        public DatePlanning GetLast(int event_id)
        {
            return _datePlannings.Where(dp => dp.Eventid == event_id && dp.start < new DateTime(2020, 6, 9))
                .OrderByDescending(dp => dp.start)
                .FirstOrDefault();
        }

        public DatePlanning GetNextEvent()
        {
            DatePlanning datePlanning = _datePlannings.Where(dp => dp.start > new DateTime(2020, 6, 9))
                .OrderBy(dp => dp.start)
                .FirstOrDefault();

            datePlanning.@event = collection.events.Where(e => e.id == datePlanning.Eventid).First();
            datePlanning.@event.genre = collection.eventGenres.Where(eg => eg.event_id == datePlanning.Eventid).ToList();

            foreach (EventGenre eventGenre in datePlanning.@event.genre)
            {
                eventGenre.genre = collection.genres.Where(g => g.id == eventGenre.genre_id).FirstOrDefault();
            }

            datePlanning.event_date = collection.eventDates.Where(ed => ed.planning_id == datePlanning.id).First();

            return datePlanning;
        }

        public DatePlanning GetUpcomming(int event_id)
        {
            DatePlanning datePlanning = _datePlannings.Where(dp => dp.Eventid == event_id && dp.start > new DateTime(2020, 6, 9)).FirstOrDefault();
            datePlanning.event_date = collection.eventDates.Where(ed => ed.planning_id == datePlanning.id).First();

            return datePlanning;
        }

        public void Update(DatePlanning date_planning)
        {
            DatePlanning datePlanning = _datePlannings.FirstOrDefault(dp => dp.id == date_planning.id);
            if (datePlanning != null)
                datePlanning = date_planning;
        }

    }
}
