namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseCreditColumnTypeUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Course", "CourseCredit", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Course", "CourseCredit", c => c.Int(nullable: false));
        }
    }
}
