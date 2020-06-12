using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class StageRepository : RepositoryBase<Stage>, IStageRepository
    {
        public StageRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Stage> GetAll()
        {
            return FindAll().Include(s=>s.schedule).ThenInclude(sc => sc.scheduleItems).ThenInclude(si => si.artist);
        }

        public IEnumerable<Stage> GetAllByEventDate(int event_date_id)
        {
            return FindByCondition(s => s.event_date_id == event_date_id)
                .Include(s => s.schedule).ThenInclude(sc => sc.scheduleItems).ThenInclude(si => si.artist);
        }

        public Stage GetById(int stage_id)
        {
            return FindByCondition(s => s.id == stage_id).FirstOrDefault();
        }

    }
}
