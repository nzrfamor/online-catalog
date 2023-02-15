using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Worker : BaseEntity
    {
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public Leader WorkerAsLeader { get; set; }
        public Subordinate WorkerAsSubordinate { get; set; }
    }
}
