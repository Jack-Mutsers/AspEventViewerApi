﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IArtistRepository Artist { get; }
        IDatePlanningRepository DatePlanning { get; }
        IEventDateRepository EventDate { get; }
        IEventRepository Event { get; }
        IGenreRepository Genre { get; }
        IPreferenceRepository Preference { get; }
        IReviewRepository Review { get; }
        IScheduleRepository Schedule { get; }
        IScheduleItemRepository ScheduleItem { get; }
        IStageRepository Stage { get; }
        IUserRepository User { get; }

        void Save();
    }
}
