namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialtableCreation : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.Course",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CourseCode = c.String(nullable: false, maxLength: 50),
                    CourseName = c.String(nullable: false, maxLength: 50),
                    CourseCredit = c.Int(nullable: false),
                    CourseDetails = c.String(maxLength: 1000),
                    DepartmentId = c.Int(nullable: false),
                    SemisterId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .ForeignKey("dbo.Semister", t => t.SemisterId)
                .Index(t => t.DepartmentId)
                .Index(t => t.SemisterId);

            CreateTable(
                "dbo.Department",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    DepartmentName = c.String(nullable: false, maxLength: 100),
                    DepartmentCode = c.String(nullable: false, maxLength: 7),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Student",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    StudentName = c.String(nullable: false, maxLength: 70),
                    Email = c.String(nullable: false, maxLength: 255),
                    ContactNo = c.String(nullable: false, maxLength: 11),
                    Date = c.DateTime(nullable: false, storeType: "date"),
                    Address = c.String(nullable: false, maxLength: 500),
                    DepartmentId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .Index(t => t.DepartmentId);

            CreateTable(
                "dbo.TeacherCourseAssign",
                c => new
                {
                    id = c.Int(nullable: false, identity: true),
                    DepartmentId = c.Int(nullable: false),
                    TeacherId = c.Int(nullable: false),
                    CreditToBeTaken = c.Int(nullable: false),
                    RemainingCredit = c.Int(nullable: false),
                    CourseId = c.Int(nullable: false),
                    CourseName = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Teacher", t => t.TeacherId)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .ForeignKey("dbo.Course", t => t.CourseId)
                .Index(t => t.DepartmentId)
                .Index(t => t.TeacherId)
                .Index(t => t.CourseId);

            CreateTable(
                "dbo.Teacher",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 100),
                    Address = c.String(nullable: false, maxLength: 1000),
                    email = c.String(nullable: false, maxLength: 255),
                    ContactNo = c.String(name: "Contact No", nullable: false, maxLength: 50),
                    Credit = c.Int(nullable: false),
                    DesignationId = c.Int(nullable: false),
                    DepartmentId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Designation", t => t.DesignationId)
                .ForeignKey("dbo.Department", t => t.DepartmentId)
                .Index(t => t.DesignationId)
                .Index(t => t.DepartmentId);

            CreateTable(
                "dbo.Designation",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    DesignationName = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Semister",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    SemisterName = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
        }
    }
}
