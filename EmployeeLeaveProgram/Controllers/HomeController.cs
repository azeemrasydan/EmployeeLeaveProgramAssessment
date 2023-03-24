using EmployeeLeaveProgram.Data;
using EmployeeLeaveProgram.Models;
using System;
using System.Net.Mail;
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

            var applicant = db.Employees.Find(employeeLeaveForm.EmployeeId);

            var manager = db.Employees.Find(employeeLeaveForm.ManagerEmployeeId);

            var leaveApplication = CommitEmployeeFormToDb(applicant, manager, employeeLeaveForm);

            SendNewLeaveApplicationEmail(
                employeeLeaveApplication: leaveApplication
                );

            return RedirectToAction("Index", "Home");
        }

        private LeaveApplication CommitEmployeeFormToDb(Employee applicant, Employee manager, EmployeeLeaveForm employeeLeaveForm)
        {

            var employeeLeaveApplication = new LeaveApplication()
            {
                Justification = employeeLeaveForm.Justification,
                StartDate = employeeLeaveForm.StartDate,
                EndDate = employeeLeaveForm.EndDate,
                Applicant = applicant,
                Manager = manager,
                Status = LeaveApplicationStatus.Pending
            };

            db.LeaveApplications.Add(employeeLeaveApplication);

            db.SaveChanges();
            return employeeLeaveApplication;
        }

        private void SendNewLeaveApplicationEmail(LeaveApplication employeeLeaveApplication)
        {
            var url = $"http://{Request.Url.Host}:{Request.UrlReferrer.Port}{Url.Action("Approval", "Manager")}";
            var body = $@"
                        <p>Leave application submitted by for your approval</p> 
                        <p><b>Requested By: </b>{employeeLeaveApplication.Applicant.FirstName} {employeeLeaveApplication.Applicant.LastName}</p>
                        <p>{url}/{employeeLeaveApplication.Id}
                        ";
            var message = new MailMessage();
            message.To.Add(new MailAddress(employeeLeaveApplication.Manager.Email)); //replace with valid value
            message.Subject = "Leave Application For Approval";
            message.Body = body;
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);

            }
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