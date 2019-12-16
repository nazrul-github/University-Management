using System.ComponentModel;
using System.Web.Mvc;

namespace University_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student name is required")]
        [StringLength(70)]
        [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z]*$", ErrorMessage = "Please enter a valid name (Only upper and lower case letters are allowed)")]
        [DisplayName("Student Name")]
        public string StudentName { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address")]
        [DisplayName("Email Address")]
        [RegularExpression(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$", ErrorMessage = "Please enter a valid email address")]
        [Remote("IsEmailExist", "Teacher", ErrorMessage = "Email already exist in database")]
        public string Email { get; set; }

        [Required]
        [StringLength(11)]
        public string ContactNo { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public List<StudentCourseAssign> StudentCourseAssigns { get; set; }
        public List<Result> Results { get; set; }

    }
}
