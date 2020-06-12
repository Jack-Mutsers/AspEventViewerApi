using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public IEnumerable<Review> GetAllOpenReviews()
        {
            return FindByCondition(r => r.validated == false);
        }

        public IEnumerable<Review> GetByEventDate(int event_date_id)
        {
            return FindByCondition(r => r.event_date_id == event_date_id && r.validated == true)
                .Include(r => r.user);
        }

        public Review GetById(int review_id)
        {
            return FindByCondition(r => r.id == review_id).FirstOrDefault();
        }

    }
}
