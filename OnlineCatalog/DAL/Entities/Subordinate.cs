using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Subordinate : BaseEntity
    {
        public Leader Leader { get; set; }
        public int LeaderId { get; set; }
        public Worker Worker { get; set; }
        public int WorkerId { get; set; }
    }
}
