namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SemesterTableRenamed : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Semester", "SemisterId", "SemesterId");
            RenameColumn("dbo.Semester", "SemisterName", "SemesterName");
        }

        public override void Down()
        {
            RenameColumn("dbo.Semester", "SemesterId", "SemisterId");
            RenameColumn("dbo.Semester", "SemesterName", "SemisterName");
        }
    }
}
