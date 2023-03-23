using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLeaveProgram.Models
{
    public class EmployeeLeaveForm
    {
        public string EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;}
        public string Justification { get; set; }
        public string ManagerEmployeeId { get; set; }

    }
}