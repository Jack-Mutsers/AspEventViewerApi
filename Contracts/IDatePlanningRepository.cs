using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IDatePlanningRepository
    {
        DatePlanning GetNextEvent();
        IEnumerable<DatePlanning> GetAll();
        IEnumerable<DatePlanning> GetAllByEvent(int event_id);
        DatePlanning GetById(int planning_id);
        DatePlanning GetUpcomming(int event_id);
        DatePlanning GetLast(int event_id);
        IEnumerable<DatePlanning> GetFinishedEventDates(int event_id);
        void CreateDatePlanning(DatePlanning date_planning);
        void UpdateDatePlanning(DatePlanning date_planning);
        void DeleteDatePlanning(DatePlanning date_planning);
    }
}
