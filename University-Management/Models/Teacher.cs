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
        public string Name { get; set; }

        [Required]
        [StringLength(1000)]
        public string Address { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        [Column("Contact No")]
        [Required]
        [StringLength(50)]
        public string Contact_No { get; set; }

        public int Credit { get; set; }

        public int DesignationId { get; set; }

        public int DepartmentId { get; set; }

       
        public virtual List<CourseAssign> CourseAssigns { get; set; }

        public virtual Department Department { get; set; }

        public virtual Designation Designation { get; set; }
    }
}
