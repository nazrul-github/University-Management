using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_Management.DAL;
using University_Management.Models;

namespace University_Management.BLL
{
    public class TeacherManager
    {
        readonly TeacherGateway _teacherGateway = new TeacherGateway();
        readonly CourseManager _courseManager = new CourseManager();
        private StudentManager _studentManager = new StudentManager();

        public List<Teacher> GetAllTeachers()
        {
            return _teacherGateway.GetAllTeachers();
        }

        public List<Teacher> GetTeacherWithDepartmentId(int departmentId)
        {
            return _teacherGateway.GetTeacherWithDepartmentId(departmentId);
        }

        public List<Designation> GetAllDesignations()
        {
            return _teacherGateway.GetAllDesignations();
        }

        public double TeacherRemainingCredit(int teacherId)
        {
            return _teacherGateway.TeacherRemainingCredit(teacherId);
        }

        public bool SaveTeacher(Teacher teacher)
        {
            return _teacherGateway.SaveTeacher(teacher);
        }

        public bool IsEmailExist(string email)
        {
            bool teacherEmail = GetAllTeachers().Any(t => t.Email.ToLower() == email.ToLower());
            bool studentEmail = _studentManager.GetAllStudents().Any(s => s.Email.ToLower() == email.ToLower());
            if (teacherEmail)
            {
                return true;
            }
            if (studentEmail)
            {
                return true;
            }

            return false;
        }
    }
}