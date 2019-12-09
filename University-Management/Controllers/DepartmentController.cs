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
    public class DepartmentController : Controller
    {
        public DepartmentManager DepartmentManager = new DepartmentManager();
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                if (DepartmentManager.IsDepartmentNameExist(department.DepartmentName))
                {
                    FlashMessage.Danger("Department Name Already Exist");
                    return View(department);
                }
                if (DepartmentManager.IsDepartmentCodeExist(department.DepartmentCode))
                {
                    FlashMessage.Danger("Department Code Already Exist");
                    return View(department);
                }
                FlashMessage.Confirmation("Department Saved Successfully");
                DepartmentManager.AddDepartment(department);
                return RedirectToAction("Create");
            }
            FlashMessage.Danger("Some error occured please check all the fields");

            return View(department);
        }


        public JsonResult IsDeptCodeExist(string DepartmentCode)
        {
            bool isExsit = DepartmentManager.IsDepartmentCodeExist(DepartmentCode);

            if (isExsit)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsDeptNameExist(string DepartmentName)
        {
            bool isExsit = DepartmentManager.IsDepartmentNameExist(DepartmentName);

            if (isExsit)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}