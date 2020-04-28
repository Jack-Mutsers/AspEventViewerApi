using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class EventGenreDto
    {
        public int event_id { get; set; }
        public int genre_id { get; set; }
        public GenreDto genre { get; set; }
    }
}
