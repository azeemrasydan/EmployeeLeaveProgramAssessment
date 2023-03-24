namespace EmployeeLeaveProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LeaveApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Justification = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Applicant_Id = c.String(maxLength: 128),
                        Manager_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.Applicant_Id)
                .ForeignKey("dbo.Employees", t => t.Manager_Id)
                .Index(t => t.Applicant_Id)
                .Index(t => t.Manager_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeaveApplications", "Manager_Id", "dbo.Employees");
            DropForeignKey("dbo.LeaveApplications", "Applicant_Id", "dbo.Employees");
            DropIndex("dbo.LeaveApplications", new[] { "Manager_Id" });
            DropIndex("dbo.LeaveApplications", new[] { "Applicant_Id" });
            DropTable("dbo.LeaveApplications");
            DropTable("dbo.Employees");
        }
    }
}
