namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsResultAvailableColumnAddedInResultTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Results", "IsResultAvailable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Results", "IsResultAvailable");
        }
    }
}
