using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Entities.DataTransferObjects
{
    public class UserDto
    {
        public string name { get; set; }
        public IEnumerable<PreferenceDto> preference { get; set; }
        public int right_id { get; set; }
        public UserRightDto right { get; set; }
    }
}
