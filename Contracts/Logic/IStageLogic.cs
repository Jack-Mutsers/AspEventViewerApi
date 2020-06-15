using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IStageLogic
    {
        IEnumerable<StageDto> GetAll();
        IEnumerable<StageDto> GetAllByEventDate(int event_date_id);
        StageDto GetById(int stage_id);
        bool Create(StageForCreationDto stageForCreation);
        bool Update(StageForUpdateDto stageForUpdate);
        bool Delete(int id);
    }
}
