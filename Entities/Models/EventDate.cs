using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("event_date")]
    public class EventDate
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Event id is required")]
        public int event_id { get; set; }

        [Required(ErrorMessage = "Planning id is required")]
        public int planning_id { get; set; }

        public string location { get; set; }

        public string images { get; set; }

        public string videos { get; set; }

        public ICollection<Stage> stages { get; set; }

        public ICollection<Review> reviews { get; set; }
    }
}
