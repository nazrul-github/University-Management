namespace University_Management.Migrations.Project_DbContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertingRoomDataToTable : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Rooms] ON
            INSERT INTO[dbo].[Rooms]([RoomId], [RoomNo]) VALUES(1, N'Room-101')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(2, N'Room-102')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(3, N'Room-103')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(4, N'Room-104')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(5, N'Room-105')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(6, N'Room-201')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(7, N'Room-202')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(8, N'Room-203')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(9, N'Room-204')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(10, N'Room-205')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(11, N'Room-301')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(12, N'Room-302')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(13, N'Room-303')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(14, N'Room-304')
            INSERT INTO[dbo].[Rooms]
                ([RoomId], [RoomNo]) VALUES(15, N'Room-305')
            SET IDENTITY_INSERT[dbo].[Rooms] OFF
            ");
        }
        
        public override void Down()
        {
        }
    }
}
