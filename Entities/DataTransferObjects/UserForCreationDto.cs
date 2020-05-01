using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Entities.DataTransferObjects
{
    public class UserForCreationDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int right_id { get; set; }
        public ICollection<PreferenceForCreationDto> preference { get; set; }
    }
}
