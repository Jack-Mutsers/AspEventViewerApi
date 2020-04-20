using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class StageDto
    {
        public int id { get; set; }
        public int event_date_id { get; set; }
        public string name { get; set; }
        public IEnumerable<Schedule> schedule { get; set; }
    }
}
