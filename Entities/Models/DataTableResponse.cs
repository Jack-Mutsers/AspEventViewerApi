using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class DataTableResponse
    {
        public int total { get; set; }  // total numer of record rows
        
        public int totalfilter { get; set; }  // total numer of record rows that match the search
        
        public object data { get; set; }  // requested data
        
    }
}
