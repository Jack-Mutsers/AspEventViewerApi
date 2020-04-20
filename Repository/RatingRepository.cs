using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void Create_Rating(Rating rating)
        {
            Create(rating);
        }

        public void Delete_Rating(Rating rating)
        {
            Create(rating);
        }

        public IEnumerable<Rating> Get_by_event_date(int event_date_id)
        {
            return FindByCondition(r => r.event_date_id == event_date_id);
        }

        public void Update_Rating(Rating rating)
        {
            Update(rating);
        }
    }
}
