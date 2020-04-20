using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class DatePlanningRepository : RepositoryBase<DatePlanning>, IDatePlanningRepository
    {
        public DatePlanningRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void Create_Date_Planning(DatePlanning date_planning)
        {
            Create(date_planning);
        }

        public void Delete_Date_Planning(DatePlanning date_planning)
        {
            Delete(date_planning);
        }

        public IEnumerable<DatePlanning> Get_All_Date_Planning(int event_id)
        {
            return FindByCondition(dp => dp.event_id == event_id);
        }

        public void Update_Date_Planning(DatePlanning date_planning)
        {
            Update(date_planning);
        }
    }
}
