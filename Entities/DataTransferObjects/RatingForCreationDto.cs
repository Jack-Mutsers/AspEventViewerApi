using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class RatingForCreationDto
    {
        public int event_date_id { get; set; }
        public int user_id { get; set; }
        public int rating { get; set; }
    }
}
