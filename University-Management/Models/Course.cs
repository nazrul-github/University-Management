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
        [StringLength(50)]
        public string CourseCode { get; set; }

        [Required]
        [StringLength(50)]
        public string CourseName { get; set; }

        public int CourseCredit { get; set; }

        [StringLength(1000)]
        public string CourseDetails { get; set; }

        public int DepartmentId { get; set; }

        public int SemisterId { get; set; }

        public virtual Semister Semister { get; set; }

        public virtual Department Department { get; set; }

      
        public virtual List<CourseAssign> CourseAssigns { get; set; }
    }
}
