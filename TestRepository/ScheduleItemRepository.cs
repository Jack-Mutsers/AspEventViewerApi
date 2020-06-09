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
    public class ScheduleItemRepository : IScheduleItemRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<ScheduleItem> _scheduleItems;

        public ScheduleItemRepository() 
        {
            _scheduleItems = collection.scheduleItems;
        }

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

        public ScheduleItem GetByIdWithDetails(int item_id)
        {
            return FindByCondition(si => si.id == item_id).Include(si => si.artist).FirstOrDefault();
        }

        public IEnumerable<ScheduleItem> GetBySchedule(int schedule_id)
        {
            return FindByCondition(si => si.schedule_id == schedule_id);
        }

        public void UpdateScheduleItem(ScheduleItem scheduleItem)
        {
            ScheduleItem scheduleItem1 = _scheduleItems.FirstOrDefault(s => s.id == scheduleItem.id);
            if (scheduleItem1 != null)
                scheduleItem1 = scheduleItem;
        }
    }
}
