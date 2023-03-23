namespace EmployeeLeaveProgram.Migrations
{
    using EmployeeLeaveProgram.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeLeaveProgram.Data.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EmployeeLeaveProgram.Data.ApplicationContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var employees = new List<Employee>
            {
                new Employee() {Id="1321",Email="abdul@gmail.com", FirstName="Abdul", LastName="Ghani"},
                new Employee() {Id="1322",Email="ang@gmail.com", FirstName="Ang", LastName="Foo"},
                new Employee() {Id="1323",Email="anu@gmail.com", FirstName="Anumugham", LastName="Muthu"}
            };
            employees.ForEach(e => context.Employees.Add(e));
            context.SaveChanges();
        }
    }
}
