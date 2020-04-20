using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class StageRepository : RepositoryBase<Stage>, IStageRepository
    {
        public StageRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void Create_Stage(Stage stage)
        {
            Create(stage);
        }

        public void Delete_Stage(Stage stage)
        {
            Delete(stage);
        }

        public IEnumerable<Stage> Get_All_By_Event_Date(int event_date_id)
        {
            return FindByCondition(s => s.event_date_id == event_date_id);
        }

        public Stage Get_By_Id(int stage_id)
        {
            return FindByCondition(s => s.id == stage_id).FirstOrDefault();
        }

        public void Update_Stage(Stage stage)
        {
            Update(stage);
        }
    }
}
