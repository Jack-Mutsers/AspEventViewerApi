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

        public void Create_schedule(Schedule schedule)
        {
            Create(schedule);
        }

        public void Delete_schedule(Schedule schedule)
        {
            Delete(schedule);
        }

        public IEnumerable<Schedule> Get_All()
        {
            return FindAll().Include(ac => ac.stage).Include(ac => ac.@event);
        }

        public Schedule Get_By_Stage(int stage_id)
        {
            return FindByCondition(s => s.stage_id == stage_id).Include(ac => ac.stage).Include(ac => ac.@event).FirstOrDefault();
        }

        public Schedule Get_By_Stage_with_details(int stage_id)
        {
            return FindByCondition(s => s.stage_id == stage_id).Include(ac => ac.scheduleItems).Include(ac => ac.stage).Include(ac => ac.@event).FirstOrDefault();
        }

        public void Update_schedule(Schedule schedule)
        {
            Update(schedule);
        }
    }
}
