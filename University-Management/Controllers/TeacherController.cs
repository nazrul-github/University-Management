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
    public class TeacherController : Controller
    {
        private TeacherManager _teacherManager = new TeacherManager();
        private DepartmentManager _departmentManager = new DepartmentManager();

        public ActionResult Create()
        {
            FillDegignationDepartment();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            FillDegignationDepartment();
            if (ModelState.IsValid)
            {
                if (_teacherManager.IsEmailExist(teacher.email))
                {
                    FlashMessage.Danger("Email already exist");
                    return View(teacher);
                }

                if (_teacherManager.SaveTeacher(teacher))
                {
                    FlashMessage.Confirmation("Teacher information saved successfully");
                    return RedirectToAction("Create");
                }
            }

            FlashMessage.Danger("Some error occured, Please check all the inputs");
            return View(teacher);
        }

        public JsonResult IsEmailExist(string email)
        {
            bool IsExist = _teacherManager.IsEmailExist(email);

            if (IsExist)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OnlyLetter(string name)
        {
            int number = 0;
            Char[] deptChar = name.ToCharArray();
            foreach (var character in deptChar)
            {
                bool isNumber = int.TryParse(character.ToString(), out number);
            }

            if (number > 0)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private void FillDegignationDepartment()
        {
            var designations = _teacherManager.GetAllDesignations();
            var department = _departmentManager.GetAllDepartments();

            ViewBag.DesignationId = new SelectList(designations, "Id", "DesignationName");
            ViewBag.DepartmentId = new SelectList(department, "DepartmentId", "DepartmentName");
        }
    }
}