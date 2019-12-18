namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeacherPropertyNameChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teacher", "Credit", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teacher", "Credit", c => c.Int(nullable: false));
        }
    }
}
