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
            /*var assingedCourse = _courseManager.GetAllAssignedCourse().Where(c=>c.TeacherId==teacherId)
            int assignedcredit = assingedCourse.;
            int teacherCredit = GetAllTeachers().FirstOrDefault(t => t.Id == teacherId).Credit;
            int remainingCredit = teacherCredit - assignedcredit;
            return remainingCredit;*/
            return _teacherGateway.TeacherRemainingCredit(teacherId);
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