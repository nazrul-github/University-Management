namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedTheIsAssignedColumn : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.StudentCourseAssigns", "IsAssinged", "IsAssigned");
            
        }
        
        public override void Down()
        {
            RenameColumn("dbo.StudentCourseAssigns", "IsAssigned", "IsAssinged");

        }
    }
}
