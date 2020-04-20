using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class PreferenceForCreationDto
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int genre_id { get; set; }
    }
}
