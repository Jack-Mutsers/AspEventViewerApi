using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IScheduleItemRepository : IUniversalRepository<ScheduleItem>
    {
        IEnumerable<ScheduleItem> GetBySchedule(int schedule_id);
        ScheduleItem GetById(int item_id);
        ScheduleItem GetByIdWithDetails(int item_id);
    }
}
