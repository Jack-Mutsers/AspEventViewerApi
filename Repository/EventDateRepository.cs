﻿using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class EventDateRepository : RepositoryBase<EventDate>, IEventDateRepository
    {
        public EventDateRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void CreateEventDate(EventDate event_date)
        {
            Create(event_date);
        }

        public void DeleteEventDate(EventDate event_date)
        {
            Delete(event_date);
        }

        public EventDate GetById(int event_date_id)
        {
            return FindByCondition(ed => ed.id == event_date_id).FirstOrDefault();
        }

        public void UpdateEventDate(EventDate event_date)
        {
            Update(event_date);
        }
    }
}
