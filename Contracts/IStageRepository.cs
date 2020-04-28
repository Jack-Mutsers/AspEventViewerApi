using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IStageRepository
    {
        IEnumerable<Stage> GetAll();
        IEnumerable<Stage> GetAllByEventDate(int event_date_id);
        Stage GetById(int stage_id);
        void CreateStage(Stage stage);
        void UpdateStage(Stage stage);
        void DeleteStage(Stage stage);
    }
}
