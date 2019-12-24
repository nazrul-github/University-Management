using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using University_Management.BLL;
using University_Management.DAL;
using University_Management.Models;
using University_Management.ViewModel;
using Vereyon.Web;

namespace University_Management.Controllers
{
    [Authorize(Roles = "robin")]
    public class CourseController : Controller
    {
        private readonly CourseManager _courseManager = new CourseManager();
        private readonly DepartmentManager _departmentManager = new DepartmentManager();

        public ActionResult Create()
        {
            FillDepartmentDropdown();
            FillSemesterDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                if (_courseManager.IsCourseCodeExist(course.CourseCode))
                {
                    FlashMessage.Danger("Course Code Already Exist");
                    return View(course);
                }
                if (_courseManager.IsCoursesNameExist(course.CourseName))
                {
                    FlashMessage.Danger("Course Name Already Exist");
                    return View(course);
                }
                if (_courseManager.SaveCourses(course))
                {
                    FlashMessage.Confirmation("Course Saved Successfully");
                    return RedirectToAction("Create");
                }
                FlashMessage.Danger("Some error occured, please try again later");
                return View();
            }
            FillDepartmentDropdown();
            FillSemesterDropdown();
            FlashMessage.Danger("Some error occured, please check all the inputs");
            return View(course);
        }
       
        public ActionResult CourseStatics()
        {
            FillDepartmentDropdown();
            return View();
        }
        public ActionResult UnAssignCourses()
        {
            return View();
        }


        //Client side validations
        public JsonResult IsCourseCodeExist(string courseCode)
        {
            bool isExist = _courseManager.IsCourseCodeExist(courseCode);
            if (isExist)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCourseNameExist(string courseName)
        {
            bool isExist = _courseManager.IsCoursesNameExist(courseName);
            if (isExist)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCourseStatics(int departmentId)
        {
            var courseStatics = _courseManager.GetCourseStatics(departmentId);
            return Json(courseStatics);
        }

        public JsonResult UnAssignAllCourse()
        {
            bool isUnAssigned = _courseManager.UnAssignAllCourse();
            return Json(isUnAssigned);
        }

        //Fill the dropdown methods
        private void FillDepartmentDropdown()
        {
            var departments = _departmentManager.GetAllDepartments();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");
        }

        private void FillSemesterDropdown()
        {
            var semesters = _courseManager.GetAllSemester();
            ViewBag.SemesterId = new SelectList(semesters, "SemesterId", "SemesterName");
        }
    }
}