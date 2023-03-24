using EmployeeLeaveProgram.Data;
using EmployeeLeaveProgram.Models;
using System;
using System.Net.Mail;
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
            var leaveApplication = db.LeaveApplications
                .Find(Int64.Parse(id));
                
            leaveApplication.Status = LeaveApplicationStatus.Approved;

            db.SaveChanges();
            SendApprovedLeaveApplicationEmail(leaveApplication);

            return RedirectToAction("Approval", "Manager");
        }

        private void SendApprovedLeaveApplicationEmail(LeaveApplication employeeLeaveApplication)
        {
            var body = $@"
                        <p>Your leave from {employeeLeaveApplication.StartDate} to {employeeLeaveApplication.EndDate} has been approved</p> 
                        ";
            var message = new MailMessage();
            message.To.Add(new MailAddress(employeeLeaveApplication.Applicant.Email)); 
            message.Subject = "Leave Application Approved";
            message.Body = body;
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);

            }
        }

        public ActionResult Reject(string id)
        {
            var leaveApplication = db.LeaveApplications
                  .Find(Int64.Parse(id));

            leaveApplication.Status = LeaveApplicationStatus.Rejected;

            db.SaveChanges();
            SendRejectedLeaveApplicationEmail(leaveApplication);

            return RedirectToAction("Approval", "Manager");
        }

        private void SendRejectedLeaveApplicationEmail(LeaveApplication employeeLeaveApplication)
        {
            var body = $@"
                        <p>Your leave from {employeeLeaveApplication.StartDate} to {employeeLeaveApplication.EndDate} has been rejected</p> 
                        ";
            var message = new MailMessage();
            message.To.Add(new MailAddress(employeeLeaveApplication.Applicant.Email));
            message.Subject = "Leave Application Rejection";
            message.Body = body;
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);

            }
        }

    }
}