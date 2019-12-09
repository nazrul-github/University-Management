using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using University_Management.Models;

namespace University_Management.EntityConfigaration
{
    public class DesignationConfigaration:EntityTypeConfiguration<Designation>
    {
        public DesignationConfigaration()
        {
            HasMany(d => d.Teachers)
                .WithRequired(t => t.Designation).HasForeignKey(t=>t.DesignationId).WillCascadeOnDelete(false);
        }
    }
}