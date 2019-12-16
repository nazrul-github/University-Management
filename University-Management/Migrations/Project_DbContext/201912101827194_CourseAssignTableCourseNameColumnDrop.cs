namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseAssignTableCourseNameColumnDrop : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TeacherCourseAssign", "CourseName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeacherCourseAssign", "CourseName", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
