using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_Management.Models;

namespace University_Management.DAL
{
    public class ClassRoomGateway
    {
        private ProjectDbContext _projectDbContext;

        public void SaveAllocatedClassRoom(AllocateClassroom classroom)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                _projectDbContext.AllocateClassrooms.Add(classroom);
                _projectDbContext.SaveChanges();
            }
        }

        public List<Room> GetAllRoomInfo()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var rooms = _projectDbContext.Rooms.ToList();
                return rooms;
            }
        }
       
    }
}