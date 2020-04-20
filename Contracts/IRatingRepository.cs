using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRatingRepository
    {
        IEnumerable<Rating> GetByEventDate(int event_date_id);
        void CreateRating(Rating rating);
    }
}
