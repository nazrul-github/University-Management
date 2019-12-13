namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InsertingsemesterdatatoColumn : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Semister] ON
INSERT INTO [dbo].[Semister] ([SemisterId], [SemisterName]) VALUES (5, N'Semester Five')
INSERT INTO [dbo].[Semister] ([SemisterId], [SemisterName]) VALUES (4, N'Semester Four')
INSERT INTO [dbo].[Semister] ([SemisterId], [SemisterName]) VALUES (6, N'Semester Six')
INSERT INTO [dbo].[Semister] ([SemisterId], [SemisterName]) VALUES (9, N'Semister Eight')
INSERT INTO [dbo].[Semister] ([SemisterId], [SemisterName]) VALUES (1, N'Semister One')
INSERT INTO [dbo].[Semister] ([SemisterId], [SemisterName]) VALUES (8, N'Semister Seven')
INSERT INTO [dbo].[Semister] ([SemisterId], [SemisterName]) VALUES (3, N'Semister Three')
INSERT INTO [dbo].[Semister] ([SemisterId], [SemisterName]) VALUES (2, N'Semister Two')
SET IDENTITY_INSERT [dbo].[Semister] OFF
");
        }

        public override void Down()
        {
        }
    }
}
