using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Student Name")]
        public string StudentName { get; set; }
        [DisplayName("Registration Number")]
        public string StudentRegistrationNumber { get; set; }
        [DisplayName("Email Address")]
        public string StudentEmail { get; set; }
        [DisplayName("Department")]
        public string StudentDepartment { get; set; }
        public List<ResultCourseView> ResultCourseViews { get; set; }
    }
}