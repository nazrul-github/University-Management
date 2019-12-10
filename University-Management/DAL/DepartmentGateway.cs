using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University_Management.Models;



namespace University_Management.DAL
{
    public class DepartmentGateway
    {
        private ProjectDbContext db;

        public List<Department> GetAllDepartments()
        {
            using (db = new ProjectDbContext())
            {
                var departments = db.Departments.ToList();
                return departments;
            }
        }

        public void AddDepartment(Department department)
        {
            using (db = new ProjectDbContext())
            {
                db.Departments.Add(department);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

       
    }
}