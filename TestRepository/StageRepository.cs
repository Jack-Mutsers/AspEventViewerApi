using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository
{
    public class StageRepository : RepositoryBase, IStageRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Stage> _stages;

        public StageRepository(RepositoryContext repositoryContext = null) : base(repositoryContext)
        {
            _stages = collection.stages;
        }

        public void Create(Stage stage)
        {
            _stages.Add(stage);
        }

        public void Delete(Stage stage)
        {
            _stages.Remove(stage);
        }

        public IEnumerable<Stage> GetAll()
        {
            List<Stage> stages = _stages;

            foreach (Stage stage in stages)
            {
                stage.schedule = collection.schedules.Where(s => s.stage_id == stage.id).ToList();

                foreach (Schedule schedule in stage.schedule)
                {
                    schedule.scheduleItems = collection.scheduleItems.Where(si => si.schedule_id == schedule.id).ToList();

                    foreach (ScheduleItem scheduleItem in schedule.scheduleItems)
                    {
                        scheduleItem.artist = collection.artists.Where(a => a.id == scheduleItem.artist_id).FirstOrDefault();
                    }
                }
            }

            return stages;
        }

        public IEnumerable<Stage> GetAllByEventDate(int event_date_id)
        {
            List<Stage> stages = _stages.Where(s => s.event_date_id == event_date_id).ToList();

            foreach (Stage stage in stages)
            {
                stage.schedule = collection.schedules.Where(s => s.stage_id == stage.id).ToList();

                foreach (Schedule schedule in stage.schedule)
                {
                    schedule.scheduleItems = collection.scheduleItems.Where(si => si.schedule_id == schedule.id).ToList();

                    foreach (ScheduleItem scheduleItem in schedule.scheduleItems)
                    {
                        scheduleItem.artist = collection.artists.Where(a => a.id == scheduleItem.artist_id).FirstOrDefault();
                    }
                }
            }

            return stages;
        }

        public Stage GetById(int stage_id)
        {
            return _stages.Where(s => s.id == stage_id).FirstOrDefault();
        }

        public void Update(Stage stage)
        {
            Stage stage1 = _stages.FirstOrDefault(s => s.id == stage.id);
            if (stage1 != null)
                stage1 = stage;
        }

    }
}
