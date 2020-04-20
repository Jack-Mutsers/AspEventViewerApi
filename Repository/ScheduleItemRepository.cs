using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ScheduleItemRepository : RepositoryBase<ScheduleItem>, IScheduleItemRepository
    {
        public ScheduleItemRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void Create_schedule_Item(ScheduleItem scheduleItem)
        {
            Create(scheduleItem);
        }

        public void Delete_schedule_Item(ScheduleItem scheduleItem)
        {
            Delete(scheduleItem);
        }

        public IEnumerable<ScheduleItem> Get_By_Schedule(int schedule_id)
        {
            return FindByCondition(si => si.schedule_id == schedule_id);
        }

        public void Update_schedule_Item(ScheduleItem scheduleItem)
        {
            Update(scheduleItem);
        }
    }
}
