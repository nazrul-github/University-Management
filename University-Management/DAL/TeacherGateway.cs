using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using University_Management.Models;

namespace University_Management.DAL
{
    public class TeacherGateway
    {
        private ProjectDbContext _projectDbContext;

        public List<Teacher> GetAllTeachers()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var teachers = _projectDbContext.Teachers.ToList();
                return teachers;
            }
        }

        public bool SaveTeacher(Teacher teacher)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                _projectDbContext.Teachers.Add(teacher);
                _projectDbContext.SaveChanges();
                int teacherId = teacher.Id;
                if (teacherId > 0)
                {
                    return true;
                }

                return false;
            }
        }

        public List<Designation> GetAllDesignations()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var designations = _projectDbContext.Designations.ToList();
                return designations;
            }
        }
        public List<Teacher> GetTeacherWithDepartmentId(int departmentId)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var teachers = _projectDbContext.Teachers.Where(t => t.DepartmentId == departmentId).ToList();
                return teachers;
            }
        }

        public double TeacherRemainingCredit(int teacherId)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                double remainingCredit = 0;
                var teacherCredit = _projectDbContext.Teachers.Find(teacherId).Credit;
                var teacherAssigned = _projectDbContext.TeacherCourseAssigns.Any(t => t.TeacherId == teacherId && t.IsAssigned);
                if (teacherAssigned)
                {
                    var assignedCredits = _projectDbContext.TeacherCourseAssigns.Where(t => t.TeacherId == teacherId && t.IsAssigned)
                        .Include(t => t.Course).Sum(t => t.Course.CourseCredit);
                     remainingCredit = teacherCredit - assignedCredits;
                     return remainingCredit;
                }
                return remainingCredit = teacherCredit;
            }

        }
    }
}