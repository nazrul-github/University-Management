namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRegistrationNumberColumnForStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentGateway", "RegistrationNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StudentGateway", "RegistrationNumber");
        }
    }
}
