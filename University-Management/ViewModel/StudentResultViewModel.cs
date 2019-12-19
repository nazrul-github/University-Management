using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace University_Management.ViewModel
{
    public class ResultCourseView
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Grade { get; set; }
    }
    public class StudentResultViewModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentRegistrationNumber { get; set; }
        public string StudentEmail { get; set; }
        public string StudentDepartment { get; set; }
        public List<ResultCourseView> ResultCourseViews { get; set; }
    }
}