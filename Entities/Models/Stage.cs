using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("stage")]
    public class Stage
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Event date id is required")]
        [ForeignKey(nameof(eventDate))]
        public int event_date_id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        public ICollection<Schedule> schedule { get; set; }
        public EventDate eventDate { get; set; }
    }
}
