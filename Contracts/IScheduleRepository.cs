using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IScheduleRepository
    {
        IEnumerable<Schedule> GetAll();
        Schedule GetByStage(int stage_id);
        Schedule GetByStageWithDetails(int stage_id);
        Schedule GetById(int Schedule_id);
        void CreateSchedule(Schedule schedule);
        void UpdateSchedule(Schedule schedule);
        void DeleteSchedule(Schedule schedule);
    }
}
