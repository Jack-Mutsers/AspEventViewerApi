using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("event")]
    public class Event
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Active is required")]
        public bool active { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Desciption is required")]
        public string description { get; set; }

        [Required(ErrorMessage = "Poster is required")]
        public string poster { get; set; }

        public ICollection<EventGenre> genre { get; set; }

        public ICollection<DatePlanning> event_planning { get; set; }
    }
}
