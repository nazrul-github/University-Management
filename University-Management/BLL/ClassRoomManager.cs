using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_Management.DAL;
using University_Management.Models;

namespace University_Management.BLL
{
    public class ClassRoomManager
    {
        ClassRoomGateway _classRoomGateway = new ClassRoomGateway();

        public void SaveAllocatedClassRoom(AllocateClassroom classroom)
        {
            _classRoomGateway.SaveAllocatedClassRoom(classroom);
        }

        public List<Room> GetAllRoomInfo()
        {
            return _classRoomGateway.GetAllRoomInfo();
        }
    }
}