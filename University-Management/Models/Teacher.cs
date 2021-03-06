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
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z.]*$", ErrorMessage = "Please enter a valid name (Only upper and lower case letters are allowed)")]
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address")]
        [DisplayName("Email Address")]
        [RegularExpression(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$", ErrorMessage = "Please enter a valid email address")]
        [Remote("IsEmailExist", "Teacher", ErrorMessage = "Email already exist in database")]
        public string Email { get; set; }

        [Column("Contact No")]
        [Required(ErrorMessage = "Please Enter a valid mobile number (01XXXXXXXXX)")]
        [StringLength(50,ErrorMessage = "Please Enter a valid mobile number (01XXXXXXXXX)")]
        [RegularExpression(@"^[0]+[1]+[0-9]{9}", ErrorMessage = "Please Enter a valid mobile number (01XXXXXXXXX)")]
        [DisplayName("Contact No")]
        public string ContactNo { get; set; }

        [DisplayName("Credit to Be Taken")]
        [Range(0, Double.PositiveInfinity, ErrorMessage = "Credit to be taken should be a positive number")]
        public double Credit { get; set; }

        public int DesignationId { get; set; }

        public int DepartmentId { get; set; }


        public List<TeacherCourseAssign> CourseAssigns { get; set; }

        public Department Department { get; set; }

        public Designation Designation { get; set; }
    }
}
