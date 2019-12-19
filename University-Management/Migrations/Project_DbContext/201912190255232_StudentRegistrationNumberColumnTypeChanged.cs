namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentRegistrationNumberColumnTypeChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "RegistrationNumber", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "RegistrationNumber", c => c.String(nullable: false));
        }
    }
}
