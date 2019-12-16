namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedGradeColumnType1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Results", "Grade", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Results", "Grade", c => c.Double(nullable: false));
        }
    }
}
