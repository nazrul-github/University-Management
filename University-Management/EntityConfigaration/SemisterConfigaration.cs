using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using University_Management.Models;

namespace University_Management.EntityConfigaration
{
    public class SemisterConfigaration:EntityTypeConfiguration<Semester>
    {
        public SemisterConfigaration()
        {
            HasMany(s=>s.Courses).WithRequired(c=>c.Semester).HasForeignKey(c=>c.SemesterId).WillCascadeOnDelete(false);
        }
    }
}