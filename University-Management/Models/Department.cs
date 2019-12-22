using System.ComponentModel;
using System.Web.Mvc;

namespace University_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Department")]
    public partial class Department
    {
        //Zebin project start
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please enter Department Name")]
        [Remote("IsDeptNameExist","Department",ErrorMessage = "Department Name already exist")]
        [DisplayName("Department Name")]
        public string DepartmentName { get; set; }

        [Required]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Department Code Should Be 2 to 7 Character Long")]
        [Remote("IsDeptCodeExist","Department",ErrorMessage = "Department code already exist")]
        [DisplayName("Department Code")]
        public string DepartmentCode { get; set; }
        //Zebin project finished



        public List<Course> Courses { get; set; }
        public List<TeacherCourseAssign> CourseAssigns { get; set; }
        public List<StudentCourseAssign> StudentCourseAssigns { get; set; }
        public List<Student> Students { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Result> Results { get; set; }

    }
}
