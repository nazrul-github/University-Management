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
        //Zebin Project Start
        private readonly ProjectDbContext _db = new ProjectDbContext();
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
                List<Department> existDep = _db.Departments.ToList();
                var matchingDep = existDep.Where(m => m.DepartmentName == department.DepartmentName || m.DepartmentCode == department.DepartmentCode);
                if (matchingDep.Any())
                {
                   FlashMessage.Danger("Name or Code Already Exist.");
                    return View(department);
                }

                _db.Departments.Add(department);
                if (_db.SaveChanges() > 0)
                {
                    FlashMessage.Confirmation("Department Saved Successfully");
                }
                

                return RedirectToAction("Create");
            }

            return View(department);
        }
        //Zebin Project Finished

        //Avi Project Start
        [Authorize(Roles = "avi,robin")]
        public ActionResult ViewDepartment()
        {
            var departments = _db.Departments.ToList();
            return View(departments);
        }
        //Avi Project Finished

        //Client Side Validation

        DepartmentManager _departmentManager = new DepartmentManager();

        public JsonResult IsDeptCodeExist(string departmentCode)
        {
            bool isExist = _departmentManager.IsDepartmentCodeExist(departmentCode);

            if (isExist)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsDeptNameExist(string departmentName)
        {
            bool isExist = _departmentManager.IsDepartmentNameExist(departmentName);

            if (isExist)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}