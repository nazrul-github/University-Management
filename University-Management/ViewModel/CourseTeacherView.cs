using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_Management.Models;

namespace University_Management.ViewModel
{
    public class CourseTeacherView
    {
        public string CourseCode { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public string AssignedTo { get; set; }

    }
}