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
    public class ScheduleRepository : IScheduleRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Schedule> _schedules;

        public ScheduleRepository() 
        {
            _schedules = collection.schedules;
        }

        public void CreateSchedule(Schedule schedule)
        {
            Create(schedule);
        }

        public void DeleteSchedule(Schedule schedule)
        {
            Delete(schedule);
        }

        public IEnumerable<Schedule> GetAll()
        {
            return FindAll().Include(ac => ac.stage).Include(ac => ac.@event);
        }

        public Schedule GetById(int schedule_id)
        {
            return FindByCondition(s => s.id == schedule_id).FirstOrDefault();
        }

        public Schedule GetByStage(int stage_id)
        {
            return FindByCondition(s => s.stage_id == stage_id).Include(ac => ac.stage).Include(ac => ac.@event).FirstOrDefault();
        }

        public Schedule GetByStageWithDetails(int stage_id)
        {
            return FindByCondition(s => s.stage_id == stage_id).Include(ac => ac.scheduleItems).Include(ac => ac.stage).Include(ac => ac.@event).FirstOrDefault();
        }

        public void UpdateSchedule(Schedule schedule)
        {
            Schedule schedule1 = _schedules.FirstOrDefault(s => s.id == schedule.id);
            if (schedule1 != null)
                schedule1 = schedule;
        }
    }
}
