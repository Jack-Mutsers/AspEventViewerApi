using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("schedule")]
    public class Schedule
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Stage id is required")]
        [ForeignKey(nameof(stage))]
        public int stage_id { get; set; }

        [Required(ErrorMessage = "Event id is required")]
        [ForeignKey(nameof(@event))]
        public int event_id { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime dateTime { get; set; }

        public ICollection<ScheduleItem> scheduleItems { get; set; }
        public Event @event { get; set; }
        public Stage stage { get; set; }
    }
}
