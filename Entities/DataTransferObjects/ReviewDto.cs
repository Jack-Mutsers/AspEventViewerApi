using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class ReviewDto
    {
        public int id { get; set; }
        public int event_date_id { get; set; }
        public int user_id { get; set; }
        public string review { get; set; }
    }
}
