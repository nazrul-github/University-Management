namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentCourseRelationTableCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentCourseAssigns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: false)
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: false)
                .Index(t => t.StudentId)
                .Index(t => t.DepartmentId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourseAssigns", "StudentId", "dbo.Student");
            DropForeignKey("dbo.StudentCourseAssigns", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.StudentCourseAssigns", "CourseId", "dbo.Course");
            DropIndex("dbo.StudentCourseAssigns", new[] { "CourseId" });
            DropIndex("dbo.StudentCourseAssigns", new[] { "DepartmentId" });
            DropIndex("dbo.StudentCourseAssigns", new[] { "StudentId" });
            DropTable("dbo.StudentCourseAssigns");
        }
    }
}
