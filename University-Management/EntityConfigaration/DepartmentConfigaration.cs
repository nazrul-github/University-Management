using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using University_Management.Models;

namespace University_Management.EntityConfigaration
{
    public class DepartmentConfigaration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfigaration()
        {
            HasMany(d => d.Courses)
                .WithRequired(c => c.Department).HasForeignKey(c=>c.DepartmentId).WillCascadeOnDelete(false);

            HasMany(d => d.CourseAssigns)
                .WithRequired(c => c.Department).HasForeignKey(c=>c.DepartmentId).WillCascadeOnDelete(false);

            HasMany(d => d.Students)
                .WithRequired(s => s.Department).HasForeignKey(s=>s.DepartmentId).WillCascadeOnDelete(false);

            HasMany(d => d.Teachers)
                    .WithRequired(t => t.Department).HasForeignKey(t=>t.DepartmentId).WillCascadeOnDelete(false);
        }
    }
}