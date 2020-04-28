using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("event_genre")]
    public class EventGenre
    {
        [ForeignKey(nameof(@event))]
        public int event_id { get; set; }
        [ForeignKey(nameof(genre))]
        public int genre_id { get; set; }
        public Event @event { get; set; }
        public Genre genre { get; set; }
    }
}
