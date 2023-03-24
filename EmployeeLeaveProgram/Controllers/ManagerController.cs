using EmployeeLeaveProgram.Data;
using EmployeeLeaveProgram.Models;
using System;
using System.Web.Mvc;

namespace EmployeeLeaveProgram.Controllers
{
    public class ManagerController : Controller
    {
        private ApplicationContext db = new ApplicationContext();

        public ActionResult Approval(string id)
        {
            if (id == null) return View();

            var leaveApplication = db.LeaveApplications.Find(Int64.Parse(id));

            if (leaveApplication == null) return View();

            return View(leaveApplication);
        }

        public ActionResult Approve(string id)
        {
            db.LeaveApplications
                .Find(Int64.Parse(id))
                .Status = LeaveApplicationStatus.Approved;

            db.SaveChanges();

            return RedirectToAction("Approval", "Manager");
        }

        public ActionResult Reject(string id)
        {
            db.LeaveApplications
                .Find(Int64.Parse(id))
                .Status = LeaveApplicationStatus.Rejected;

            db.SaveChanges();

            return RedirectToAction("Approval", "Manager");
        }

    }
}