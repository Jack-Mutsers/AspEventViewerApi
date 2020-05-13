using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    [Table("genre")]
    public class Genre
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        public EventGenre eventGenre { get; set; }

        public ArtistGenre artistGenre { get; set; }
    }
}
