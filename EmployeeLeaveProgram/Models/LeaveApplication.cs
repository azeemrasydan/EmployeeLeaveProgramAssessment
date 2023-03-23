using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeLeaveProgram.Models
{
    public enum LeaveApplicationStatus
    {
        Pending,
        Approved,
        Rejected
    }

    public class LeaveApplication
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Justification { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual Employee Applicant { get; set; }
        public virtual Employee Manager { get; set; }
        public LeaveApplicationStatus Status { get; set; }
    }
}