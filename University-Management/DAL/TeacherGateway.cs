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
                if (teacherId > 0)
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
        public List<Teacher> GetTeacherWithDepartmentId(int departmentId)
        {
            using (projectDbContext = new ProjectDbContext())
            {
                var teachers = projectDbContext.Teachers.Where(t => t.DepartmentId == departmentId).ToList();
                return teachers;
            }
        }

        public double TeacherRemainingCredit(int teacherId)
        {
            using (projectDbContext = new ProjectDbContext())
            {
                var courseAssigned = projectDbContext.TeacherCourseAssigns.Where(c => c.IsAssigned);
                var teacherWithCreditAssigned = courseAssigned.Join(projectDbContext.Teachers, c => c.TeacherId, t => t.Id,
                    ((assign, teacher) => new
                    {
                        TeacherId = teacher.Id,
                        CourseId = assign.CourseId
                    }));
                var teacherAssignedCredit = teacherWithCreditAssigned.Join(projectDbContext.Courses, t => t.CourseId, c => c.Id, (a, course) => new
                {
                    TeacherId = a.TeacherId,
                    TotalCredit = course.CourseCredit
                }).ToList();
                double assignedCredit = teacherAssignedCredit.Where(t => t.TeacherId == teacherId).Sum(t => t.TotalCredit);
                double credit = projectDbContext.Teachers.Find(teacherId).Credit;

                double remainingCredit = credit - assignedCredit;
                return remainingCredit;
            }

        }
    }
}