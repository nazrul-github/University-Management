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

        public bool IsClassRoomAvailable(string classroomDay, DateTime classroomFromTime, DateTime classroomToTime)
        {
            var rooms = _classRoomGateway.GetAllAllocatedClassRoom().Where(c => c.Day.ToLower() == classroomDay.ToLower()).ToList();
            foreach (var room in rooms)
            {
                string fromtimeDb = room.FromTime.ToString("HH.mm");
                string totimeDb = room.ToTime.ToString("HH.mm");
                string usrFrmTime = classroomFromTime.ToString("HH.mm");
                string usrToTime = classroomToTime.ToString("HH.mm");
                if (Convert.ToDouble(usrFrmTime)<=Convert.ToDouble(totimeDb) && Convert.ToDouble(fromtimeDb)<=Convert.ToDouble(usrToTime))
                {
                    return false;
                }
            }

            return true;
        }
    }
}