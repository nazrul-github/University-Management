namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseAssignTableIsAssignedTypeChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseAssign", "IsAssigned", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CourseAssign", "IsAssigned", c => c.Boolean(nullable: false));
        }
    }
}
