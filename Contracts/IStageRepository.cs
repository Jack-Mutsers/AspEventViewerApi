using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IStageRepository
    {
        IEnumerable<Stage> Get_All_By_Event_Date(int event_date_id);
        Stage Get_By_Id(int stage_id);
        void Create_Stage(Stage stage);
        void Update_Stage(Stage stage);
        void Delete_Stage(Stage stage);
    }
}
