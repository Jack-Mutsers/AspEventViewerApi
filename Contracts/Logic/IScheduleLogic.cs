using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IScheduleLogic
    {
        IEnumerable<ScheduleDto> GetAll();
        ScheduleDto GetByStage(int stage_id);
        ScheduleDto GetByStageWithDetails(int stage_id);
        ScheduleDto GetById(int Schedule_id);
        bool Create(ScheduleForCreationDto scheduleForCreation);
        bool Update(ScheduleForUpdateDto scheduleForUpdate);
        bool Delete(int id);
    }
}
