using System.ComponentModel;
using System.Web.Mvc;

namespace University_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Teacher")]
    public partial class Teacher
    {

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Remote("OnlyLetter","Teacher",ErrorMessage = "Please enter only letters")]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.EmailAddress,ErrorMessage = "Please enter a valid email address")]
        [DisplayName("Email Address")]
        [RegularExpression(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$",ErrorMessage = "Please enter a valid email address")]
        [Remote("IsEmailExist","Teacher",ErrorMessage = "Email already exist")]
        public string email { get; set; }

        [Column("Contact No")]
        [Required]
        [StringLength(50)]
        [DisplayName("Contact No")]
        public string Contact_No { get; set; }

        [DisplayName("Credit to Be Taken")]
        [Range(0,Double.PositiveInfinity,ErrorMessage = "Credit to be taken should be a positive number")]
        public int Credit { get; set; }

        public int DesignationId { get; set; }

        public int DepartmentId { get; set; }

       
        public  List<CourseAssign> CourseAssigns { get; set; }

        public  Department Department { get; set; }

        public  Designation Designation { get; set; }
    }
}
