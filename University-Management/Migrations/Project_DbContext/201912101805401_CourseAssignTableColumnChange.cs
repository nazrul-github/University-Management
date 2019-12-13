namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseAssignTableColumnChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TeacherCourseAssign", "IsAssigned", c => c.Boolean(nullable: false));
            DropColumn("dbo.TeacherCourseAssign", "CreditToBeTaken");
            DropColumn("dbo.TeacherCourseAssign", "CourseCredit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeacherCourseAssign", "CourseCredit", c => c.Int(nullable: false));
            AddColumn("dbo.TeacherCourseAssign", "CreditToBeTaken", c => c.Int(nullable: false));
            AlterColumn("dbo.TeacherCourseAssign", "IsAssigned", c => c.Boolean());
        }
    }
}
