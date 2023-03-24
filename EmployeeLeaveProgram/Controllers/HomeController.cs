using EmployeeLeaveProgram.Data;
using EmployeeLeaveProgram.Models;
using System;
using System.Web.Mvc;

namespace EmployeeLeaveProgram.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db = new ApplicationContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            var employeeLeaveForm = new EmployeeLeaveForm()
            {
                EmployeeId = Request["employeeId"],
                StartDate = Convert.ToDateTime(Request["startDate"]),
                EndDate = Convert.ToDateTime(Request["endDate"]),
                Justification = Request["justification"],
                ManagerEmployeeId = Request["managerEmployeeId"]

            };

            CommitEmployeeFormToDb(employeeLeaveForm);

            return RedirectToAction("Index", "Home");
        }

        private int CommitEmployeeFormToDb(EmployeeLeaveForm employeeLeaveForm)
        {
            var applicant = db.Employees.Find(employeeLeaveForm.EmployeeId);
            var manager = db.Employees.Find(employeeLeaveForm.ManagerEmployeeId);

            var employeeLeaveApplication = new LeaveApplication() { 
                Justification = employeeLeaveForm.Justification,
                StartDate = employeeLeaveForm.StartDate,
                EndDate = employeeLeaveForm.EndDate,
                Applicant = applicant,
                Manager = manager,
                Status = LeaveApplicationStatus.Pending
            };

            db.LeaveApplications.Add(employeeLeaveApplication);

            return db.SaveChanges(); ;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}