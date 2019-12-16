namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedGradeColumnType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Results", "Grade", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Results", "Grade", c => c.Int(nullable: false));
        }
    }
}
