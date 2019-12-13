namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsAllocatedColumnAddedInAssignClassRoom : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AllocateClassrooms", "IsAllocated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AllocateClassrooms", "IsAllocated");
        }
    }
}
