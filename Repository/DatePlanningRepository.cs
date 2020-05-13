using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class DatePlanningRepository : RepositoryBase<DatePlanning>, IDatePlanningRepository
    {
        public DatePlanningRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void CreateDatePlanning(DatePlanning date_planning)
        {
            Create(date_planning);
        }

        public void DeleteDatePlanning(DatePlanning date_planning)
        {
            Delete(date_planning);
        }

        public IEnumerable<DatePlanning> GetAll()
        {
            return FindAll()
                .Include(dp => dp.@event).ThenInclude(e => e.genre).ThenInclude(eg => eg.genre)
                .Include(dp => dp.event_date);
        }

        public IEnumerable<DatePlanning> GetAllByEvent(int event_id)
        {
            return FindByCondition(dp => dp.Eventid == event_id)
                .Include(dp => dp.event_date);
        }

        public DatePlanning GetById(int planning_id)
        {
            return FindByCondition(dp => dp.id == planning_id)
                .Include(dp => dp.event_date)
                .FirstOrDefault();
        }

        public DatePlanning GetByIdWithDetails(int planning_id)
        {
            //return FindByCondition(dp => dp.id == planning_id)
            //    .Include(dp => dp.@event).ThenInclude(e => e.genre).ThenInclude(eg => eg.genre)
            //    .Include(dp => dp.event_date).ThenInclude(ed => ed.stages).ThenInclude(st => st.schedule).ThenInclude(sc => sc.scheduleItems).ThenInclude(si => si.artist)
            //    .Include(dp => dp.event_date).ThenInclude(ed => ed.reviews.Where(r => r.validated == true)).ThenInclude(r => r.user)
            //    .FirstOrDefault();

            return FindByCondition(dp => dp.id == planning_id)
                .Include(dp => dp.@event).ThenInclude(e => e.genre).ThenInclude(eg => eg.genre)
                .Include(dp => dp.event_date)//.ThenInclude(ed => ed.stages).ThenInclude(st => st.schedule).ThenInclude(sc => sc.scheduleItems).ThenInclude(si => si.artist)
                .FirstOrDefault();

        }

        public IEnumerable<DatePlanning> GetFinishedEventDates(int event_id)
        {
            return FindByCondition(dp => dp.Eventid == event_id && dp.start < DateTime.Now)
                .Include(dp => dp.event_date)
                .OrderByDescending(dp => dp.start);
        }

        public DatePlanning GetLast(int event_id)
        {
            return FindByCondition(dp => dp.Eventid == event_id && dp.start < DateTime.Now)
                .OrderByDescending(dp => dp.start)
                .FirstOrDefault();
        }

        public DatePlanning GetNextEvent()
        {
            return FindByCondition(dp => dp.start > DateTime.Now)
                .OrderBy(dp => dp.start)
                .Include(dp => dp.@event).ThenInclude(e => e.genre).ThenInclude(eg => eg.genre)
                .Include(dp => dp.event_date)
                .FirstOrDefault();
        }

        public DatePlanning GetUpcomming(int event_id)
        {
            return FindByCondition(dp => dp.Eventid == event_id && dp.start > DateTime.Now)
                .Include(dp => dp.event_date)
                .FirstOrDefault();
        }

        public void UpdateDatePlanning(DatePlanning date_planning)
        {
            Update(date_planning);
        }
    }
}
