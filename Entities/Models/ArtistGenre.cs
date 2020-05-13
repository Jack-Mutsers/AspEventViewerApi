using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("artist_genre")]
    public class ArtistGenre
    {
        [ForeignKey(nameof(artist))]
        public int artist_id { get; set; }

        [ForeignKey(nameof(genre))]
        public int genre_id { get; set; }

        public Artist artist { get; set; }

        public Genre genre { get; set; }
    }
}
