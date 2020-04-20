using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("rating")]
    public class Rating
    {
        [Required(ErrorMessage = "Event date id is required")]
        public int event_date_id { get; set; }

        [Required(ErrorMessage = "User id is required")]
        public int user_id { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        public int rating { get; set; }
    }
}
