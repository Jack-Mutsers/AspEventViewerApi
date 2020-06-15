using Entities.DataTransferObjects;
using System.Collections.Generic;

namespace Contracts.Logic
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
