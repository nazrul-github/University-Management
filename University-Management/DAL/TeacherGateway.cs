using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_Management.Models;

namespace University_Management.DAL
{
    public class TeacherGateway
    {
        private ProjectDbContext projectDbContext;

        public List<Teacher> GetAllTeachers()
        {
            using (projectDbContext = new ProjectDbContext())
            {
                var teachers = projectDbContext.Teachers.ToList();
                return teachers;
            }
        }

        public bool SaveTeacher(Teacher teacher)
        {
            using (projectDbContext = new ProjectDbContext())
            {
                projectDbContext.Teachers.Add(teacher);
                projectDbContext.SaveChanges();
                int teacherId = teacher.Id;
                if (teacherId>0)
                {
                    return true;
                }

                return false;
            }
        }

        public List<Designation> GetAllDesignations()
        {
            using (projectDbContext = new ProjectDbContext())
            {
                var designations = projectDbContext.Designations.ToList();
                return designations;
            }
        }
    }
}