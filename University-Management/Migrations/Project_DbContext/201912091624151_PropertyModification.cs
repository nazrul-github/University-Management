namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropertyModification : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CourseAssign", "RemainingCredit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseAssign", "RemainingCredit", c => c.Int(nullable: false));
        }
    }
}
