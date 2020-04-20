using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRatingRepository
    {
        IEnumerable<Rating> Get_by_event_date(int event_date_id);
        void Create_Rating(Rating rating);
        void Update_Rating(Rating rating);
        void Delete_Rating(Rating rating);
    }
}
