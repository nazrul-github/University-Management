namespace University_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Semester")]
    public class Semester
    {

        [Key]
        public int SemesterId { get; set; }

        [Required]
        [StringLength(50)]
        public string SemesterName { get; set; }


        public List<Course> Courses { get; set; }
    }
}
