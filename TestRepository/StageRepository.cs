﻿using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository
{
    public class StageRepository : IStageRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Stage> _stages;

        public StageRepository()
        {
            _stages = collection.stages;
        }

        public void CreateStage(Stage stage)
        {
            Create(stage);
        }

        public void DeleteStage(Stage stage)
        {
            Delete(stage);
        }

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

        public void UpdateStage(Stage stage)
        {
            Stage stage1 = _stages.FirstOrDefault(s => s.id == stage.id);
            if (stage1 != null)
                stage1 = stage;
        }
    }
}