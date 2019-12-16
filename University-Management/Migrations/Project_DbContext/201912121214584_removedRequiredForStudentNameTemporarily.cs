namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedRequiredForStudentNameTemporarily : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Student", newName: "StudentGateway");
            AlterColumn("dbo.StudentGateway", "StudentName", c => c.String(maxLength: 70));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentGateway", "StudentName", c => c.String(nullable: false, maxLength: 70));
            RenameTable(name: "dbo.StudentGateway", newName: "Student");
        }
    }
}
