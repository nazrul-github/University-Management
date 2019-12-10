namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseAssignTableCourseNameColumnDrop : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CourseAssign", "CourseName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseAssign", "CourseName", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
