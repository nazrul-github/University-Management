namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsAssignedColumnAddedToStudentCourseAssing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentCourseAssigns", "IsAssigned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentCourseAssigns", "IsAssigned");
        }
    }
}
