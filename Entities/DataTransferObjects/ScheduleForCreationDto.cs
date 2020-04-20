using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class ScheduleForCreationDto
    {
        public int id { get; set; }
        public int stage_id { get; set; }
        public int event_id { get; set; }
        public DateTime dateTime { get; set; }
    }
}
