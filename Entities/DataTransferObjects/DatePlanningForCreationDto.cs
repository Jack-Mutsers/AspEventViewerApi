using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class DatePlanningForCreationDto
    {
        public int id { get; set; }
        public int event_id { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}
