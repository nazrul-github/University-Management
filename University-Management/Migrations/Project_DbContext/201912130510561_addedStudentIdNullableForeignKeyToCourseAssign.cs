namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedStudentIdNullableForeignKeyToCourseAssign : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherCourseAssign", "StudentId", c => c.Int());
            CreateIndex("dbo.TeacherCourseAssign", "StudentId");
            AddForeignKey("dbo.TeacherCourseAssign", "StudentId", "dbo.Student", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherCourseAssign", "StudentId", "dbo.Student");
            DropIndex("dbo.TeacherCourseAssign", new[] { "StudentId" });
            DropColumn("dbo.TeacherCourseAssign", "StudentId");
        }
    }
}
