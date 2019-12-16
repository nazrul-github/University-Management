namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsAssignedColumnAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeacherCourseAssign", "IsAssigned", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeacherCourseAssign", "IsAssigned");
        }
    }
}
