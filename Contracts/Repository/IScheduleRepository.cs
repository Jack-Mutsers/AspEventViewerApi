using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IScheduleRepository : IUniversalRepository<Schedule>
    {
        IEnumerable<Schedule> GetAll();
        Schedule GetByStage(int stage_id);
        Schedule GetByStageWithDetails(int stage_id);
        Schedule GetById(int Schedule_id);
    }
}
