﻿using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IEventDateRepository : IUniversalRepository<EventDate>
    {
        EventDate GetById(int event_date_id);
        EventDate GetByDatePlanning(int date_planning_id);
        EventDate GetByIdWithDetails(int event_date_id);
    }
}
