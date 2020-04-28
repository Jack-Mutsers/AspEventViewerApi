using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class DatePlanningForUpdateDto
    {
        public int id { get; set; }
        public int Eventid { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}
