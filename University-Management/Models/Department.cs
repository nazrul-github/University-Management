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
       public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Department Name")]
        [Remote("IsDeptNameExist","Department",ErrorMessage = "Department Name Already Exist")]
        public string DepartmentName { get; set; }

        [Required]
        [StringLength(7)]
        [DisplayName("Department Code")]
        [Remote("IsDeptCodeExist","Department",ErrorMessage = "Department Code Already Exist")]
        public string DepartmentCode { get; set; }

       
        public virtual List<Course> Courses { get; set; }

       
        public virtual List<CourseAssign> CourseAssigns { get; set; }

       
        public virtual List<Student> Students { get; set; }

      
        public virtual List<Teacher> Teachers { get; set; }
    }
}
