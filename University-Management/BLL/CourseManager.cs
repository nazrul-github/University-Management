using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_Management.DAL;
using University_Management.Models;

namespace University_Management.BLL
{
    public class CourseManager
    {
        readonly CourseGateway _courseGateway = new CourseGateway();

        public List<Course> GetAllCourses()
        {
            return _courseGateway.GetAllCourses();
        }

        public List<Semister> GetAllSemester()
        {
            return _courseGateway.GetAllSemester();
        }

        public List<CourseAssign> GetAllAssignedCourse()
        {
            return _courseGateway.GetAllAssignedCourse();
        }

        public List<Course> GetAllCoursesByDepartmentId(int departmentId)
        {
            return _courseGateway.GetAllCoursesByDepartmentId(departmentId);
        }

        public bool IsCourseAssigned(int courseId)
        {
            bool isExist = GetAllAssignedCourse().Any(c => c.CourseId == courseId);

            return isExist;
        }

        public bool SaveCourses(Course course)
        {
            return _courseGateway.CreateCourse(course);
        }

        public bool IsCourseCodeExist(string courseCode)
        {
            var courseCd = courseCode.Replace(" ", String.Empty);
            var courses = GetAllCourses().ToList();

            var isExist = GetAllCourses()
                .Any(c => c.CourseCode.Replace(" ", String.Empty).ToLower() == courseCd.ToLower());

            return isExist;
        }

        public bool IsCoursesNameExist(string courseName)
        {
            var courseNm = courseName.Replace(" ", String.Empty);
            var isExist = GetAllCourses()
                .Any(c => c.CourseName.Replace(" ", String.Empty).ToLower() == courseNm.ToLower());

            return isExist;
        }

        public bool AssignCourse(CourseAssign courseAssign)
        {
            return _courseGateway.AssignCourse(courseAssign);
        }
    }
}