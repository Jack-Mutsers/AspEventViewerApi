﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("date_planning")]
    public class DatePlanning
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Event id is required")]
        public int Eventid { get; set; }

        [Required(ErrorMessage = "Start datetime is required")]
        public DateTime start { get; set; }

        [Required(ErrorMessage = "End datetime is required")]
        public DateTime end { get; set; }

        public EventDate event_date { get; set; }
        public Event @event { get; set; }
    }
}
