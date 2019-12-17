namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoursePropertyNameChange : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Semester", newName: "Semester");
           
            RenameColumn(table: "dbo.Course", name: "SemesterId", newName: "SemesterId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Course", name: "SemesterId", newName: "SemesterId");
          
            RenameTable(name: "dbo.Semester", newName: "Semester");
        }
    }
}
