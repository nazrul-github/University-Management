using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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

        public List<AllocateClassroom> GetAllAllocatedClassRoom()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var rooms = _projectDbContext.AllocateClassrooms.Where(ac => ac.IsAllocated).ToList();
                return rooms;
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

        public List<AllocateClassroom> GetAllocatedClassroomsByDepartment(int departmentId)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var departmentroom = _projectDbContext.AllocateClassrooms
                    .Include(a => a.Course).Include(a => a.Room)
                    .Where(a => a.DepartmentId == departmentId && a.IsAllocated).ToList();

                return departmentroom;
            }

        }

        public bool UnAllocateClassRooms()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var allocatedClassrooms = _projectDbContext.AllocateClassrooms.Where(a=>a.IsAllocated).ToList();
                foreach (var classroom in allocatedClassrooms)
                {
                    classroom.IsAllocated = false;
                }
                _projectDbContext.SaveChanges();
                return true;
            }
        }
    }

}

