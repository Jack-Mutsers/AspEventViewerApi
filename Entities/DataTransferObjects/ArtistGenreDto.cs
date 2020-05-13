using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class ArtistGenreDto
    {
        public int artist_id { get; set; }
        public int genre_id { get; set; }
        public GenreDto genre { get; set; }
    }
}
