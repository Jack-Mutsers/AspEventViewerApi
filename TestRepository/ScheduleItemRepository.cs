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
    public class ScheduleItemRepository : RepositoryBase, IScheduleItemRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<ScheduleItem> _scheduleItems;

        public ScheduleItemRepository(RepositoryContext repositoryContext = null) : base(repositoryContext)
        {
            _scheduleItems = collection.scheduleItems;
        }

        public void Create(ScheduleItem scheduleItem)
        {
            _scheduleItems.Add(scheduleItem);
        }

        public void Delete(ScheduleItem scheduleItem)
        {
            _scheduleItems.Remove(scheduleItem);
        }

        public ScheduleItem GetById(int item_id)
        {
            return _scheduleItems.Where(si => si.id == item_id).FirstOrDefault();
        }

        public ScheduleItem GetByIdWithDetails(int item_id)
        {
            ScheduleItem scheduleItem = _scheduleItems.Where(si => si.id == item_id).FirstOrDefault();

            scheduleItem.artist = collection.artists.Where(a => a.id == scheduleItem.artist_id).FirstOrDefault();

            return scheduleItem;
        }

        public IEnumerable<ScheduleItem> GetBySchedule(int schedule_id)
        {
            return _scheduleItems.Where(si => si.schedule_id == schedule_id);
        }

        public void Update(ScheduleItem scheduleItem)
        {
            ScheduleItem scheduleItem1 = _scheduleItems.FirstOrDefault(s => s.id == scheduleItem.id);
            if (scheduleItem1 != null)
                scheduleItem1 = scheduleItem;
        }

    }
}
