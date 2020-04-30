using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class UserRightDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool admin { get; set; }
    }
}
