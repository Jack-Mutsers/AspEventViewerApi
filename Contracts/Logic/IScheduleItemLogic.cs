using Entities.DataTransferObjects;
using System.Collections.Generic;

namespace Contracts.Logic
{
    public interface IScheduleItemLogic
    {
        IEnumerable<ScheduleItemDto> GetBySchedule(int schedule_id);
        ScheduleItemDto GetById(int item_id);
        ScheduleItemDto GetByIdWithDetails(int item_id);
        bool Create(ScheduleItemForCreationDto scheduleItemForCreation);
        bool Update(ScheduleItemForUpdateDto scheduleItemForUpdate);
        bool Delete(int id);
    }
}
