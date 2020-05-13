using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class EventForCreationDto
    {
        public bool active { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string poster { get; set; }
        public IEnumerable<EventGenreForCreationDto> genre { get; set; }
    }
}
