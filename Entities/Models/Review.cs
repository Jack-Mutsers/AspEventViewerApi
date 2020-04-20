using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("review")]
    public class Review
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Event date id is required")]
        public int event_date_id { get; set; }

        [Required(ErrorMessage = "User id is required")]
        public int user_id { get; set; }

        [Required(ErrorMessage = "Review is required")]
        public string review { get; set; }

        public bool validated { get; set; }
    }
}
