using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Entities.DataTransferObjects
{
    public class UserDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public IEnumerable<PreferenceDto> preference { get; set; }
    }
}
