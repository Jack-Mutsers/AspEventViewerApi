using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IScheduleItemRepository
    {
        IEnumerable<ScheduleItem> Get_By_Schedule(int schedule_id);
        void Create_schedule_Item(ScheduleItem scheduleItem);
        void Update_schedule_Item(ScheduleItem scheduleItem);
        void Delete_schedule_Item(ScheduleItem scheduleItem);
    }
}
