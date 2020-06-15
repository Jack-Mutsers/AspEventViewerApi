using Contracts.Repository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class ScheduleItemRepository : RepositoryBase<ScheduleItem>, IScheduleItemRepository
    {
        public ScheduleItemRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

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

    }
}
