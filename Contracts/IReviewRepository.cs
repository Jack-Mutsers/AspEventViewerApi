using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IReviewRepository : IUniversalRepository<Review>
    {
        IEnumerable<Review> GetByEventDate(int event_date_id);
        IEnumerable<Review> GetAllOpenReviews();
        Review GetById(int review_id);
    }
}
