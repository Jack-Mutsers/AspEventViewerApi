using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ArtistForCreationDto
    {
        public string name { get; set; }
        public IEnumerable<ArtistGenreForCreationDto> genre { get; set; }
    }
}
