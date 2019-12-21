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
    [Authorize]
    public class CourseController : Controller
    {
        //Sakib Project Start

        private ProjectDbContext db = new ProjectDbContext();

        [Authorize(Roles = "sakib,robin")]
        public ActionResult Create()
        {

            ViewBag.Departmentid = new SelectList(db.Departments, "Departmentid", "DepartmentName");
            ViewBag.Semesterid = new SelectList(db.Semisters, "Semesterid", "SemesterName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course saveCourse)
        {
            if (ModelState.IsValid)
            {
                var isSaved = db.Courses.ToList();
                var check = isSaved.Where(m => m.CourseCode == saveCourse.CourseCode || m.CourseName == saveCourse.CourseName);
                if (check.Any())
                {
                    FlashMessage.Danger("Course Name Or Course Code Already Exist");
                    return View(saveCourse);
                }
                db.Courses.Add(saveCourse);
                db.SaveChanges();
                FlashMessage.Confirmation("Course Saved Successfully");
                return RedirectToAction("Create");
            }

            ViewBag.Departmentid = new SelectList(db.Departments, "Departmentid", "DepartmentName", saveCourse.DepartmentId);
            ViewBag.Semesterid = new SelectList(db.Semisters, "Semesterid", "SemesterName", saveCourse.SemesterId);
            FlashMessage.Danger("Some error occured, please check all the inputs");
            return View(saveCourse);
        }

        //Sakib Project Finished


        private readonly CourseManager _courseManager = new CourseManager();
        private readonly DepartmentManager _departmentManager = new DepartmentManager();

        [Authorize(Roles = "robin")]
        public ActionResult CourseStatics()
        {
            FillDepartmentDropdown();
            return View();
        }
        [Authorize(Roles = "robin")]
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }


}