namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamingstudentTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.StudentGateway", newName: "Student");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Student", newName: "StudentGateway");
        }
    }
}
