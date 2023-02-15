using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class SubordinateModel
    {
        public int Id { get; set; }
        public int LeaderId { get; set; }
        public int WorkerId { get; set; }
    }
}
