using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IEventDateLogic
    {
        EventDateDto GetById(int event_date_id);
        EventDateDto GetByDatePlanning(int date_planning_id);
        EventDateDto GetByIdWithDetails(int event_date_id);
        bool Create(EventDateForCreationDto eventDateForCreation);
        bool Update(EventDateForUpdateDto eventDateForUpdate);
        bool Delete(int id);
    }
}
