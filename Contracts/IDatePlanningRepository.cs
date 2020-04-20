using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IDatePlanningRepository
    {
        IEnumerable<DatePlanning> GetAllByEvent(int event_id);
        DatePlanning GetById(int planning_id);
        void CreateDatePlanning(DatePlanning date_planning);
        void UpdateDatePlanning(DatePlanning date_planning);
        void DeleteDatePlanning(DatePlanning date_planning);
    }
}
