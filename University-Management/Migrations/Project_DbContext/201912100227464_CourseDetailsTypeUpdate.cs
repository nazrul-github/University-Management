﻿namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseDetailsTypeUpdate : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Semester", "Id", "SemesterId");
           
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Semester", "SemesterId", "Id");

        }
    }
}
