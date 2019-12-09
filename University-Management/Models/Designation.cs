namespace University_Management.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Designation")]
    public partial class Designation
    {
        

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string DesignationName { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
