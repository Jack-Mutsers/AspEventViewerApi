using Contracts;
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

        public void Create_Event_Date(EventDate event_date)
        {
            Create(event_date);
        }

        public void Delete_Event_Date(EventDate event_date)
        {
            Delete(event_date);
        }

        public EventDate Get_Event_Date_By_Id(int event_date_id)
        {
            return FindByCondition(ed => ed.id == event_date_id).FirstOrDefault();
        }

        public void Update_Event_Date(EventDate event_date)
        {
            Update(event_date);
        }
    }
}
