using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repository
{
    public interface IReviewLogic
    {
        IEnumerable<ReviewDto> GetByEventDate(int event_date_id);
        IEnumerable<ReviewDto> GetAllOpenReviews();
        ReviewDto GetById(int review_id);
        bool Create(ReviewForCreationDto reviewForCreation);
        bool Update(ReviewForUpdateDto reviewForUpdate);
        bool Delete(int id);
    }
}
