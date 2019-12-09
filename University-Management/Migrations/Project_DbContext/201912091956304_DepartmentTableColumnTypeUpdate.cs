namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DepartmentTableColumnTypeUpdate : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Department", "Id", "DepartmentId");
            
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Department", "DepartmentId", "Id");

        }
    }
}
