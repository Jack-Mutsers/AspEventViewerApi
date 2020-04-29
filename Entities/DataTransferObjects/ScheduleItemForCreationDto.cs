using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class ScheduleItemForCreationDto
    {
        public int schedule_id { get; set; }
        public int artist_id { get; set; }
        public DateTime start { get; set; }
        public int stage_time { get; set; }
    }
}
