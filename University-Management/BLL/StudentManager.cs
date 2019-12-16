using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_Management.DAL;
using University_Management.Models;

namespace University_Management.BLL
{
    public class StudentManager
    {
        readonly StudentGateway _studentGateway = new StudentGateway();

        public List<Student> GetAllStudents()
        {
            return _studentGateway.GetAllStudents();
        }

        public int AddStudent(Student student)
        {
           return _studentGateway.AddStudent(student);
        }

        public bool UpdateStudent(int id, string combined)
        {
          return  _studentGateway.UpdateStudent(id, combined);
        }
        public bool IsStudentAssigned(int courseAssignStudentId, int courseAssignCourseId)
        {
            return _studentGateway.IsStudentAssigned(courseAssignCourseId, courseAssignStudentId);
        }

        public bool EnrollStudentToCourse(StudentCourseAssign courseAssign)
        {
            return _studentGateway.EnrollStudentToCourse(courseAssign);
        }
    }
}