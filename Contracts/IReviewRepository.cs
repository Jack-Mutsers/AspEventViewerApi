using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IReviewRepository
    {
        IEnumerable<Review> Get_By_Event_Date(int event_date_id);
        IEnumerable<Review> Get_All_Open_Reviews();
        void Create_Review(Review review);
        void Update_Review(Review review);
        void Delete_Review(Review review);
    }
}
