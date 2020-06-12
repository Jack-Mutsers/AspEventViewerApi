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
    public class ReviewRepository : RepositoryBase, IReviewRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Review> _reviews;

        public ReviewRepository(RepositoryContext repositoryContext = null) : base(repositoryContext)
        {
            _reviews = collection.reviews;
        }

        public void Create(Review review)
        {
            _reviews.Add(review);
        }

        public void Delete(Review review)
        {
            _reviews.Remove(review);
        }

        public IEnumerable<Review> GetAllOpenReviews()
        {
            return _reviews.Where(r => r.validated == false);
        }

        public IEnumerable<Review> GetByEventDate(int event_date_id)
        {
            List<Review> reviews = _reviews.Where(r => r.event_date_id == event_date_id && r.validated == true).ToList();

            foreach (Review review in reviews)
            {
                review.user = collection.users.Where(u=>u.id == review.user_id).FirstOrDefault();
            }

            return reviews;
        }

        public Review GetById(int review_id)
        {
            return _reviews.Where(r => r.id == review_id).FirstOrDefault();
        }

        public void Update(Review review)
        {
            Review review1 = _reviews.FirstOrDefault(r => r.id == review.id);
            if (review1 != null)
                review1 = review;
        }

    }
}
