using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace University_Management.Models
{
    public class Result
    {
        [Key]
        public int Id { get; set; }

        public int DepartmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Grade { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }
        public Department Department { get; set; }

    }
}