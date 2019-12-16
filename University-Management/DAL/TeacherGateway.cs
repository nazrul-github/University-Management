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
            /*var assingedCourse = _courseManager.GetAllAssignedCourse().Where(c=>c.TeacherId==teacherId)
            int assignedcredit = assingedCourse.;
            int teacherCredit = GetAllTeachers().FirstOrDefault(t => t.Id == teacherId).Credit;
            int remainingCredit = teacherCredit - assignedcredit;
            return remainingCredit;*/
            using (projectDbContext = new ProjectDbContext())
            {
                var courseAssinged = projectDbContext.TeacherCourseAssigns.Where(c=>c.IsAssigned);
                var teacherWithCreditAssigned = courseAssinged.Join(projectDbContext.Teachers, c => c.TeacherId, t => t.Id,
                    ((assign, teacher) => new
                    {
                        TeacherId = teacher.Id,
                        CourseId = assign.CourseId
                    }));
                var teacherAssingedCredit = teacherWithCreditAssigned.Join(projectDbContext.Courses, t => t.CourseId, c => c.Id, (a, course) => new
                {
                    TeacherId = a.TeacherId,
                    TotalCredit = course.CourseCredit
                }).ToList();
                double assignedCredit = teacherAssingedCredit.Where(t => t.TeacherId == teacherId).Sum(t => t.TotalCredit);
                double credit = projectDbContext.Teachers.Find(teacherId).Credit;

                double remainingCredit = credit - assignedCredit;
                return remainingCredit;
            }
           
        }
    }
}