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

        public List<Teacher> GetAllTeachers()
        {
            return _teacherGateway.GetAllTeachers();
        }

        public List<Designation> GetAllDesignations()
        {
            return _teacherGateway.GetAllDesignations();
        }

        public bool SaveTeacher(Teacher teacher)
        {
            return _teacherGateway.SaveTeacher(teacher);
        }

        public bool IsEmailExist(string email)
        {
            bool teacherEmail = GetAllTeachers().Any(t => t.email.ToLower() == email.ToLower());

            if (teacherEmail)
            {
                return true;
            }

            return false;
        }
    }
}