using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class OrderRequest
    {
        public string FieldName { get; set; }
        public bool Ascending { get; set; }
    }
}
