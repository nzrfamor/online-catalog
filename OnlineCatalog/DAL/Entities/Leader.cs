using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Leader : BaseEntity
    {
        public ICollection<Subordinate> Subordinates { get; set; }
        public Worker Worker { get; set; }
        public int WorkerId { get; set; }
    }
}
