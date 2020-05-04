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
        public UserRightDto right { get; set; }
        public IEnumerable<PreferenceDto> preference { get; set; }
    }
}
