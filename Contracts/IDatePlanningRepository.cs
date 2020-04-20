using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IDatePlanningRepository
    {
        IEnumerable<DatePlanning> Get_All_Date_Planning(int event_id);
        void Create_Date_Planning(DatePlanning date_planning);
        void Update_Date_Planning(DatePlanning date_planning);
        void Delete_Date_Planning(DatePlanning date_planning);
    }
}
