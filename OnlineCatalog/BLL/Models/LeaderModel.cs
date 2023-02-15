using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class LeaderModel
    {
        public int Id { get; set; }
        public ICollection<int> SubordinatesIds { get; set; }
        public int WorkerId { get; set; }
    }
}
