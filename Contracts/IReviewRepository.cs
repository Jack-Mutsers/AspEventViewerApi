using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetByEventDate(int event_date_id);
        IEnumerable<Review> GetAllOpenReviews();
        Review GetById(int review_id);
        void CreateReview(Review review);
        void UpdateReview(Review review);
        void DeleteReview(Review review);
    }
}
