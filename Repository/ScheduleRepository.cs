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
    public class ScheduleRepository : RepositoryBase<Schedule>, IScheduleRepository
    {
        public ScheduleRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

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
            Update(schedule);
        }
    }
}
