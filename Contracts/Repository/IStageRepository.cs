using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IStageRepository : IUniversalRepository<Stage>
    {
        IEnumerable<Stage> GetAll();
        IEnumerable<Stage> GetAllByEventDate(int event_date_id);
        Stage GetById(int stage_id);
    }
}
