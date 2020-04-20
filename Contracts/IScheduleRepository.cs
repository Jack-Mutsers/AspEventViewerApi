using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IScheduleRepository
    {
        IEnumerable<Schedule> Get_All();
        Schedule Get_By_Stage(int stage_id);
        Schedule Get_By_Stage_with_details(int stage_id);
        void Create_schedule(Schedule schedule);
        void Update_schedule(Schedule schedule);
        void Delete_schedule(Schedule schedule);
    }
}
