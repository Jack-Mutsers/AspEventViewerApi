using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("artist")]
    public class Artist
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        public ICollection<ArtistGenre> genre { get; set; }

        public ScheduleItem ScheduleItem { get; set; }
    }
}
