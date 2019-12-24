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
        readonly DepartmentGateway _departmentGateway = new DepartmentGateway();
        public List<Department> GetAllDepartments()
        {
            return _departmentGateway.GetAllDepartments();
        }

        public bool AddDepartment(Department department)
        { 
          return  _departmentGateway.AddDepartment(department);
        }
        
        

        public bool IsDepartmentCodeExist(string departmentCode)
        {
           var deptCode = departmentCode.Replace(" ", String.Empty);
           var isExist = GetAllDepartments()
               .Any(d => d.DepartmentCode.Replace(" ", String.Empty).ToLower() == deptCode.ToLower());

           return isExist;
        }

        public bool IsDepartmentNameExist(string departmentName)
        {
            var deptName = departmentName.Replace(" ", String.Empty);
            var isExist = GetAllDepartments()
                .Any(d => d.DepartmentName.Replace(" ", String.Empty).ToLower() == deptName.ToLower());

            return isExist;
        }

       
    }
}