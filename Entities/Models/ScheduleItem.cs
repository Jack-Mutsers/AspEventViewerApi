using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("schedule_item")]
    public class ScheduleItem
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Schedule id is required")]
        [ForeignKey(nameof(schedule))]
        public int schedule_id { get; set; }
        
        [Required(ErrorMessage = "artist id is required")]
        [ForeignKey(nameof(artist))]
        public int artist_id { get; set; }

        [Required(ErrorMessage = "Artist is required")]
        public Artist artist { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public int stage_time { get; set; }
        public Schedule schedule { get; set; }
    }
}
