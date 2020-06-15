using Entities.DataTransferObjects;
using System.Collections.Generic;

namespace Contracts.Logic
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
        bool Create(DatePlanningForCreationDto datePlanningForCreation);
        bool Update(DatePlanningForUpdateDto datePlanningForUpdate);
        bool Delete(int id);
    }
}
