namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllocateClassRoomTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllocateClassrooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        Day = c.String(nullable: false, maxLength: 15),
                        FromTime = c.DateTime(nullable: false),
                        ToTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Department", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.CourseId)
                .Index(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AllocateClassrooms", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.AllocateClassrooms", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.AllocateClassrooms", "CourseId", "dbo.Course");
            DropIndex("dbo.AllocateClassrooms", new[] { "RoomId" });
            DropIndex("dbo.AllocateClassrooms", new[] { "CourseId" });
            DropIndex("dbo.AllocateClassrooms", new[] { "DepartmentId" });
            DropTable("dbo.AllocateClassrooms");
        }
    }
}
