using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class PreferenceDto
    {
        public int id { get; set; }
        public GenreDto genre { get; set; }
    }
}
