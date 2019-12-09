using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using University_Management.Models;

namespace University_Management.EntityConfigaration
{
    public class CourseConfigaration:EntityTypeConfiguration<Course>
    {
        public CourseConfigaration()
        {
            HasMany(c => c.CourseAssigns)
                .WithRequired(c => c.Course).HasForeignKey(c=>c.CourseId).WillCascadeOnDelete(false);
        }
    }
}