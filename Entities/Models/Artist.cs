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


        [Required(ErrorMessage = "genre id is required")]
        [ForeignKey(nameof(genre))]
        public int genre_id { get; set; }

        public Genre genre { get; set; }
    }
}
