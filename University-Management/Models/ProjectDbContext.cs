using University_Management.EntityConfigaration;

namespace University_Management.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ProjectDbContext : DbContext
    {
        public ProjectDbContext()
            : base("name=ProjectDbContext")
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<TeacherCourseAssign> CourseAssigns { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Designation> Designations { get; set; }
        public virtual DbSet<Semister> Semisters { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<AllocateClassroom> AllocateClassrooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourseConfigaration());
            modelBuilder.Configurations.Add(new DepartmentConfigaration());
            modelBuilder.Configurations.Add(new DesignationConfigaration());
            modelBuilder.Configurations.Add(new SemisterConfigaration());
            modelBuilder.Configurations.Add(new TeacherConfigaration());
        }
    }
}
