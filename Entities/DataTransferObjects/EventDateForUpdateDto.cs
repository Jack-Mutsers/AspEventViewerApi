using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class EventDateForUpdateDto
    {
        public int id { get; set; }
        public int event_id { get; set; }
        public int planning_id { get; set; }
        public string location { get; set; }
        public string poster { get; set; }
        public string images { get; set; }
        public string videos { get; set; }
        public string order_link { get; set; }
    }
}
