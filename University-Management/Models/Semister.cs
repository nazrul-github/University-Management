namespace University_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Semister")]
    public partial class Semister
    {
       
        [Key]
        public int SemisterId { get; set; }

        [Required]
        [StringLength(50)]
        public string SemisterName { get; set; }

      
        public virtual List<Course> Courses { get; set; }
    }
}
