using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class EventDto
    {
        public int id { get; set; }
        public bool active { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string poster { get; set; }
        public IEnumerable<EventGenreDto> genre { get; set; }
        public DatePlanningDto next { get; set; }
        public IEnumerable<DatePlanningDto> finished { get; set; }
    }
}
