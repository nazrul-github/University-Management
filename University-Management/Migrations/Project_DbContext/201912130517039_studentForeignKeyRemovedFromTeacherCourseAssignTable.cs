namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentForeignKeyRemovedFromTeacherCourseAssignTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeacherCourseAssign", "StudentId", "dbo.Student");
            RenameTable(name: "dbo.TeacherCourseAssign", newName: "TeacherCourseAssign");
            DropIndex("dbo.TeacherCourseAssign", new[] { "StudentId" });
            DropColumn("dbo.TeacherCourseAssign", "StudentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TeacherCourseAssign", "StudentId", c => c.Int());
            CreateIndex("dbo.TeacherCourseAssign", "StudentId");
            RenameTable(name: "dbo.TeacherCourseAssign", newName: "TeacherCourseAssign");
            AddForeignKey("dbo.TeacherCourseAssign", "StudentId", "dbo.Student", "Id");
        }
    }
}
