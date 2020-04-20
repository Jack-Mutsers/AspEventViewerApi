using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ArtistDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int genre_id { get; set; }
    }
}
