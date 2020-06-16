using Entities.DataTransferObjects;
using System.Collections.Generic;

namespace Contracts.Logic
{
    public interface IScheduleLogic
    {
        IEnumerable<ScheduleDto> GetAll();
        IEnumerable<ScheduleDto> GetByStage(int stage_id);
        IEnumerable<ScheduleDto> GetByStageWithDetails(int stage_id);
        ScheduleDto GetById(int Schedule_id);
        bool Create(ScheduleForCreationDto scheduleForCreation);
        bool Update(ScheduleForUpdateDto scheduleForUpdate);
        bool Delete(int id);
    }
}
