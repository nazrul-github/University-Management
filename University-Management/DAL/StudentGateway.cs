using System;
using System.Collections.Generic;
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
                var students = _projectDbContext.Students.ToList();
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
                _projectDbContext.SaveChanges();
                return true;
            }
        }
    }
}