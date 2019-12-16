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
                double fu = Convert.ToDouble(usrFrmTime);
                double tu = Convert.ToDouble(usrToTime);
                double fd = Convert.ToDouble(fromtimeDb);
                double td = Convert.ToDouble(totimeDb);
                if ((fu < fd && fu < td)&&(tu < td && tu > fd))
                {
                    return false;
                }else if ((fu == fd && fu < td) || (tu == td && tu > fd))
                {
                    return false;
                }else if ((fu < fd && fu < td) && (tu < td && tu > fd
                          ))
                {
                    return false;
                }else if ((fu < fd && fu < td) && (tu > fd && tu > td))
                {
                    return false;
                }else if ((fu > fd && fu < td) && (tu > fd && tu > td))
                {
                    return false;
                }
            }

            return true;
        }

        public List<AllocateClassroom> GetAllocatedClassroomsByDepartment(int departmentId)
        {
            return _classRoomGateway.GetAllocatedClassroomsByDepartment(departmentId);
        }

        public bool UnAllocateClassRooms()
        {
           return _classRoomGateway.UnAllocateClassRooms();
        }
    }
}