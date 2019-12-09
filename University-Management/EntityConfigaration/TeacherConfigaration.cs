using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using University_Management.Models;

namespace University_Management.EntityConfigaration
{
    public class TeacherConfigaration:EntityTypeConfiguration<Teacher>
    {
        public TeacherConfigaration()
        {
            HasMany(t=>t.CourseAssigns)
                .WithRequired(c=>c.Teacher).HasForeignKey(c=>c.TeacherId).WillCascadeOnDelete(false);
        }
    }
}