using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University_Management.BLL;
using University_Management.Models;
using Vereyon.Web;

namespace University_Management.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly DepartmentManager _departmentManager = new DepartmentManager();

        [Authorize(Roles = "zebin,robin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "zebin,robin")]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                if (_departmentManager.IsDepartmentNameExist(department.DepartmentName))
                {
                    FlashMessage.Danger("Department Name Already Exist");
                    return View(department);
                }
                if (_departmentManager.IsDepartmentCodeExist(department.DepartmentCode))
                {
                    FlashMessage.Danger("Department Code Already Exist");
                    return View(department);
                }
                FlashMessage.Confirmation("Department Saved Successfully");
                _departmentManager.AddDepartment(department);
                return RedirectToAction("Create");
            }
            FlashMessage.Danger("Some error occured please check all the fields");

            return View(department);
        }
        [Authorize(Roles = "avi,robin")]

        public ActionResult ViewDepartment()
        {
            var departments = _departmentManager.GetAllDepartments();
            return View(departments);
        }


        //Client Side Validation

        public JsonResult IsDeptCodeExist(string departmentCode)
        {
            bool isExist = _departmentManager.IsDepartmentCodeExist(departmentCode);

            if (isExist)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsDeptNameExist(string departmentName)
        {
            bool isExist = _departmentManager.IsDepartmentNameExist(departmentName);

            if (isExist)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}