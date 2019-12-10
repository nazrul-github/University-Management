namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoruseCreditColumnAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseAssign", "CourseCredit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseAssign", "CourseCredit");
        }
    }
}
