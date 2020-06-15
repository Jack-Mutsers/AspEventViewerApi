using Contracts;
using Contracts.Repository;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository
{
    public class ScheduleRepository : RepositoryBase, IScheduleRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Schedule> _schedules;

        public ScheduleRepository(RepositoryContext repositoryContext = null) : base(repositoryContext)
        {
            _schedules = collection.schedules;
        }

        public void Create(Schedule schedule)
        {
            _schedules.Add(schedule);
        }

        public void Delete(Schedule schedule)
        {
            _schedules.Remove(schedule);
        }

        public IEnumerable<Schedule> GetAll()
        {
            List<Schedule> schedules = _schedules;

            foreach (Schedule schedule in schedules)
            {
                schedule.stage = collection.stages.Where(s => s.id == schedule.stage_id).FirstOrDefault();
                schedule.@event = collection.events.Where(e => e.id == schedule.event_id).FirstOrDefault();
            }

            return schedules;
        }

        public Schedule GetById(int schedule_id)
        {
            return _schedules.Where(s => s.id == schedule_id).FirstOrDefault();
        }

        public Schedule GetByStage(int stage_id)
        {
            Schedule schedule = _schedules.Where(s => s.stage_id == stage_id).FirstOrDefault();

            schedule.stage = collection.stages.Where(s => s.id == schedule.stage_id).FirstOrDefault();
            schedule.@event = collection.events.Where(e => e.id == schedule.event_id).FirstOrDefault();

            return schedule;
        }

        public Schedule GetByStageWithDetails(int stage_id)
        {
            Schedule schedule = _schedules.Where(s => s.stage_id == stage_id).FirstOrDefault();

            schedule.scheduleItems = collection.scheduleItems.Where(si => si.schedule_id == schedule.id).ToList();
            schedule.stage = collection.stages.Where(s => s.id == schedule.stage_id).FirstOrDefault();
            schedule.@event = collection.events.Where(e => e.id == schedule.event_id).FirstOrDefault();

            return schedule;
        }

        public void Update(Schedule schedule)
        {
            Schedule schedule1 = _schedules.FirstOrDefault(s => s.id == schedule.id);
            if (schedule1 != null)
                schedule1 = schedule;
        }

    }
}
