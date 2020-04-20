using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("preference")]
    public class Preference
    {
        public int id { get; set; }

        [Required(ErrorMessage = "User id is required")]
        public int user_id { get; set; }
        
        [Required(ErrorMessage = "User id is required")]
        [ForeignKey(nameof(genre))]
        public int genre_id { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public Genre genre { get; set; }
    }
}
