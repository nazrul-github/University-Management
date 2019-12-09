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
            bool isExist = departmentGateway.GetAllDepartments()
                .Any(d => d.DepartmentCode.ToLower() == departmentCode.ToLower());

            if (isExist)
            {
                return true;
            }

            return false;
        }

        public bool IsDepartmentNameExist(string departmentName)
        {
            bool isExist = departmentGateway.GetAllDepartments()
                .Any(d => d.DepartmentName.ToLower() == departmentName.ToLower());

            if (isExist)
            {
                return true;
            }

            return false;
        }
    }
}