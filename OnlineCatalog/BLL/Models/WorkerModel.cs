using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class WorkerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public int? LeaderId { get; set; }
        public int? WorkerAsLeaderId { get; set; }
        public int? WorkerAsSubordinateId { get; set; }
        public IEnumerable<int> SubordinatesIds { get; set; }
    }
}
