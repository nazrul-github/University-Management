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
        private ProjectDbContext _db;

        public List<Department> GetAllDepartments()
        {
            using (_db = new ProjectDbContext())
            {
                var departments = _db.Departments.ToList();
                return departments;
            }
        }

        public void AddDepartment(Department department)
        {
            using (_db = new ProjectDbContext())
            {
                _db.Departments.Add(department);
                try
                {
                    _db.SaveChanges();
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