using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using University_Management.Models;

namespace University_Management.DAL
{
    public class StudentGateway
    {
        private ProjectDbContext _projectDbContext;

        public List<Student> GetAllStudents()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var students = _projectDbContext.Students.Include(c=>c.Department).OrderByDescending(s=>s.Id).ToList();
                return students;
            }
        }

        public int AddStudent(Student student)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                _projectDbContext.Students.Add(student);
                _projectDbContext.SaveChanges();
                int id = student.Id;
                return id;
            }
        }

        public bool UpdateStudent(int id, string combined)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var student = _projectDbContext.Students.Find(id);
                if (student != null) student.RegistrationNumber = combined;
                int rowAffected = _projectDbContext.SaveChanges();
                if (rowAffected>0)
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsStudentAssigned(int courseAssignCourseId, int courseAssignStudentId)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                bool isExist =  _projectDbContext.StudentCourseAssigns.Any(c =>
                    c.IsAssigned && c.CourseId == courseAssignCourseId && c.StudentId == courseAssignStudentId);
                return isExist;
            }
        }

        public bool EnrollStudentToCourse(StudentCourseAssign courseAssign)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                _projectDbContext.StudentCourseAssigns.Add(courseAssign);
                _projectDbContext.SaveChanges();
                int id = courseAssign.Id;
                if (id>0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}