using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("user_right")]
    public class UserRight
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool admin { get; set; }
    }
}
