using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public void Create_Review(Review review)
        {
            Create(review);
        }

        public void Delete_Review(Review review)
        {
            Delete(review);
        }

        public IEnumerable<Review> Get_All_Open_Reviews()
        {
            return FindByCondition(r => r.validated == false);
        }

        public IEnumerable<Review> Get_By_Event_Date(int event_date_id)
        {
            return FindByCondition(r => r.event_date_id == event_date_id && r.validated == true);
        }

        public void Update_Review(Review review)
        {
            Update(review);
        }
    }
}
