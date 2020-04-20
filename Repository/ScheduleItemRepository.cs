using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ScheduleItemRepository : RepositoryBase<ScheduleItem>, IScheduleItemRepository
    {
        public ScheduleItemRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void CreateScheduleItem(ScheduleItem scheduleItem)
        {
            Create(scheduleItem);
        }

        public void DeleteScheduleItem(ScheduleItem scheduleItem)
        {
            Delete(scheduleItem);
        }

        public ScheduleItem GetById(int item_id)
        {
            return FindByCondition(si => si.id == item_id).FirstOrDefault();
        }

        public IEnumerable<ScheduleItem> GetBySchedule(int schedule_id)
        {
            return FindByCondition(si => si.schedule_id == schedule_id);
        }

        public void UpdateScheduleItem(ScheduleItem scheduleItem)
        {
            Update(scheduleItem);
        }
    }
}
