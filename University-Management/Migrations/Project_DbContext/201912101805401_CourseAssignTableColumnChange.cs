namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseAssignTableColumnChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CourseAssign", "IsAssigned", c => c.Boolean(nullable: false));
            DropColumn("dbo.CourseAssign", "CreditToBeTaken");
            DropColumn("dbo.CourseAssign", "CourseCredit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CourseAssign", "CourseCredit", c => c.Int(nullable: false));
            AddColumn("dbo.CourseAssign", "CreditToBeTaken", c => c.Int(nullable: false));
            AlterColumn("dbo.CourseAssign", "IsAssigned", c => c.Boolean());
        }
    }
}
