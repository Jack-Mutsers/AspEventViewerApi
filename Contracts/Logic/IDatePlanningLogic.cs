using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IDatePlanningLogic
    {
        IEnumerable<DatePlanningDto> GetAll();
        IEnumerable<DatePlanningDto> GetAllByEvent(int event_id);
        IEnumerable<DatePlanningDto> GetFinishedEventDates(int event_id);
        DatePlanningDto GetNextEvent();
        DatePlanningDto GetById(int planning_id);
        DatePlanningDto GetByIdWithDetails(int planning_id);
        DatePlanningDto GetUpcomming(int event_id);
        DatePlanningDto GetLast(int event_id);
        bool Create(DatePlanningForCreationDto datePlanningForCreation);
        bool Update(DatePlanningForUpdateDto datePlanningForUpdate);
        bool Delete(int id);
    }
}
