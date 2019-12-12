using System.Web.Mvc;

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

        [Remote("IsCourseAssigned","Course",ErrorMessage = "This course already been assigned")]
        public int CourseId { get; set; }

        public bool IsAssigned { get; set; }

        public Course Course { get; set; }

        
        public Department Department { get; set; }

        public Teacher Teacher { get; set; }
    }
}
