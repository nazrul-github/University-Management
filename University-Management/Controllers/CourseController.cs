using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University_Management.BLL;
using University_Management.DAL;
using University_Management.Models;
using Vereyon.Web;

namespace University_Management.Controllers
{
    public class CourseController : Controller
    {
        CourseManager CourseManager = new CourseManager();
        DepartmentManager departmentManager = new DepartmentManager();

        public ActionResult Create()
        {
            FillDepartmentAndSemesterDropdown();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            FillDepartmentAndSemesterDropdown();

            if (ModelState.IsValid)
            {

                if (CourseManager.IsCoursesNameExist(course.CourseName))
                {
                    FlashMessage.Danger("Course Name Already Exist");
                    return View(course);
                }

                if (CourseManager.IsCourseCodeExist(course.CourseCode))
                {
                    FlashMessage.Danger("Course Code Already Exist");
                    return View(course);
                }

                if (CourseManager.SaveCourses(course))
                {
                    FlashMessage.Confirmation("Course Saved Successfully");
                    return RedirectToAction("Create");
                }
            }

            FlashMessage.Danger("Some error occured,Please Check all the input field");
            return View();
        }

        public JsonResult IsCourseCodeExist(string courseCode)
        {
            bool isExist = CourseManager.IsCourseCodeExist(courseCode);
            if (isExist)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCourseNameExist(string courseName)
        {
            bool isExist = CourseManager.IsCoursesNameExist(courseName);
            if (isExist)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private void FillDepartmentAndSemesterDropdown()
        {
            var departments = departmentManager.GetAllDepartments();
            var semesters = CourseManager.GetAllSemester();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");
            ViewBag.SemisterId = new SelectList(semesters, "SemisterId", "SemisterName");
        }
    }


}