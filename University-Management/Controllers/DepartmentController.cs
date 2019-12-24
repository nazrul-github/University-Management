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

    [Authorize(Roles = "robin")]
    public class DepartmentController : Controller
    {
        readonly DepartmentManager _departmentManager = new DepartmentManager();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                if (_departmentManager.IsDepartmentCodeExist(department.DepartmentCode))
                {
                    FlashMessage.Danger("Department code already exist");
                    return View(department);
                }

                if (_departmentManager.IsDepartmentNameExist(department.DepartmentName))
                {
                    FlashMessage.Danger("Department name already exist");
                    return View(department);
                }

                if (_departmentManager.AddDepartment(department))
                {
                    FlashMessage.Confirmation("Department saved successfully");
                    return RedirectToAction("Create");
                }
                FlashMessage.Danger("Some error occured, please try again later");
                return View(department);
            }
            FlashMessage.Danger("Some error occured please check all the inputs.");
            return View(department);
        }

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