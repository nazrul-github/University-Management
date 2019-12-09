using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_Management.DAL;
using University_Management.Models;

namespace University_Management.BLL
{
    public class DepartmentManager
    {
        DepartmentGateway departmentGateway = new DepartmentGateway();
        public List<Department> GetAllDepartments()
        {
            return departmentGateway.GetAllDepartments();
        }

        public void AddDepartment(Department department)
        { 
            
            departmentGateway.AddDepartment(department);
        }

        public bool IsDepartmentCodeExist(string departmentCode)
        {
           var deptCode = departmentCode.Replace(" ", String.Empty);
            var deparments = departmentGateway.GetAllDepartments().ToList();
            var isExist = false;
            foreach (var deparment in deparments)
            {
               var deptCodeDb = deparment.DepartmentCode.Replace(" ", String.Empty);
                if (deptCodeDb.ToLower() == deptCode.ToLower())
                {
                    isExist = true;
                }
            }
               

            if (isExist)
            {
                return true;
            }

            return false;
        }

        public bool IsDepartmentNameExist(string departmentName)
        {
            var deptName = departmentName.Replace(" ", String.Empty);
            var deparments = departmentGateway.GetAllDepartments().ToList();
            var isExist = false;
            foreach (var deparment in deparments)
            {
                var deptNameDb = deparment.DepartmentName.Replace(" ", String.Empty);
                if (deptNameDb.ToLower() == deptName.ToLower())
                {
                    isExist = true;
                }
            }

            if (isExist)
            {
                return true;
            }

            return false;
        }
    }
}