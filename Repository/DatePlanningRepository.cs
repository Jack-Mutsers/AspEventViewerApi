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

        public void CreateDatePlanning(DatePlanning date_planning)
        {
            Create(date_planning);
        }

        public void DeleteDatePlanning(DatePlanning date_planning)
        {
            Delete(date_planning);
        }

        public IEnumerable<DatePlanning> GetAllByEvent(int event_id)
        {
            return FindByCondition(dp => dp.event_id == event_id);
        }

        public DatePlanning GetById(int planning_id)
        {
            return FindByCondition(dp => dp.id == planning_id).FirstOrDefault();
        }

        public void UpdateDatePlanning(DatePlanning date_planning)
        {
            Update(date_planning);
        }
    }
}
