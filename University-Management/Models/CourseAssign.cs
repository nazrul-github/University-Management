namespace University_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseAssign")]
    public partial class CourseAssign
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public int TeacherId { get; set; }

        public int CreditToBeTaken { get; set; }

        public int CourseId { get; set; }

        [Required]
        [StringLength(50)]
        public string CourseName { get; set; }

        public virtual Course Course { get; set; }

        
        public virtual Department Department { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
