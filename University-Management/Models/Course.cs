using System.ComponentModel;
using System.Web.Mvc;

namespace University_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
       

        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 5,ErrorMessage = "Course code should be at least 5 character long and unique")]
        [DisplayName("Course Code")]
        [Remote("IsCourseCodeExist", "Course", ErrorMessage = "Course code already exist")]
        public string CourseCode { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Course Name")]
        [Remote("IsCourseNameExist","Course",ErrorMessage = "Course name already exist")]
        public string CourseName { get; set; }

        [DisplayName("Course Credit")]
        [Range(0.5,5.0,ErrorMessage = "Course credit should be between 0.5 - 5.0")]
        public double CourseCredit { get; set; }

        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        [DisplayName("Course Details")]
        public string CourseDetails { get; set; }

        public int DepartmentId { get; set; }

        public int SemisterId { get; set; }

        public Semister Semister { get; set; }

        public Department Department { get; set; }

      
        public List<TeacherCourseAssign> CourseAssigns { get; set; }
        public List<StudentCourseAssign> StudentCourseAssigns { get; set; }
    }
}
