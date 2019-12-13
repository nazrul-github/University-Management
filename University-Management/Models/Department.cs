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
        [Key]
       public int DepartmentId { get; set; }

        [Required]
        [DisplayName("Department Name")]
        [Remote("IsDeptNameExist","Department",ErrorMessage = "Department Name Already Exist")]
        public string DepartmentName { get; set; }

        [Required]
        [StringLength(7,MinimumLength = 2,ErrorMessage = "Department Code Should Be 2 to 7 Character Long")]
        [DisplayName("Department Code")]
        [Remote("IsDeptCodeExist","Department",ErrorMessage = "Department Code Already Exist")]
        public string DepartmentCode { get; set; }

       
        public  List<Course> Courses { get; set; }

       
        public  List<TeacherCourseAssign> CourseAssigns { get; set; }

       
        public  List<Student> Students { get; set; }

      
        public  List<Teacher> Teachers { get; set; }
    }
}
