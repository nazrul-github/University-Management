namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InsertingsemesterdatatoColumn : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Semester] ON
INSERT INTO [dbo].[Semester] ([SemesterId], [SemesterName]) VALUES (5, N'Semester Five')
INSERT INTO [dbo].[Semester] ([SemesterId], [SemesterName]) VALUES (4, N'Semester Four')
INSERT INTO [dbo].[Semester] ([SemesterId], [SemesterName]) VALUES (6, N'Semester Six')
INSERT INTO [dbo].[Semester] ([SemesterId], [SemesterName]) VALUES (9, N'Semester Eight')
INSERT INTO [dbo].[Semester] ([SemesterId], [SemesterName]) VALUES (1, N'Semester One')
INSERT INTO [dbo].[Semester] ([SemesterId], [SemesterName]) VALUES (8, N'Semester Seven')
INSERT INTO [dbo].[Semester] ([SemesterId], [SemesterName]) VALUES (3, N'Semester Three')
INSERT INTO [dbo].[Semester] ([SemesterId], [SemesterName]) VALUES (2, N'Semester Two')
SET IDENTITY_INSERT [dbo].[Semester] OFF
");
        }

        public override void Down()
        {
        }
    }
}
