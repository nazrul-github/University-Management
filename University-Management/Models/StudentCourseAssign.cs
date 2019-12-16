using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace University_Management.Models
{
    public class StudentCourseAssign
    {
        [Key]
        public int Id { get; set; }

        public int StudentId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsAssigned { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}