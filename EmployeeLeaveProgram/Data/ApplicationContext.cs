using EmployeeLeaveProgram.Models;
using System.Data.Entity;

namespace EmployeeLeaveProgram.Data
{
    public class ApplicationContext : DbContext
    {

        public ApplicationContext() : base("ApplicationContext") 
        { 

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

    }
}