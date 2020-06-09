﻿using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestRepository
{
    public class ReviewRepository : IReviewRepository
    {
        RepositoryCollection collection = new RepositoryCollection();
        private readonly List<Review> _reviews;

        public ReviewRepository() 
        {
            _reviews = collection.reviews;
        }

        public void CreateReview(Review review)
        {
            Create(review);
        }

        public void DeleteReview(Review review)
        {
            Delete(review);
        }

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

        public void UpdateReview(Review review)
        {
            Review review1 = _reviews.FirstOrDefault(r => r.id == review.id);
            if (review1 != null)
                review1 = review;
        }
    }
}