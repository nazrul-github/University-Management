using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using University_Management.BLL;
using University_Management.DAL;
using University_Management.Models;
using Vereyon.Web;

namespace University_Management.Controllers
{
    public class CourseController : Controller
    {
        readonly CourseManager _courseManager = new CourseManager();
        readonly DepartmentManager _departmentManager = new DepartmentManager();
        readonly TeacherManager _teacherManager = new TeacherManager();

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

                if (_courseManager.IsCoursesNameExist(course.CourseName))
                {
                    FlashMessage.Danger("Course Name Already Exist");
                    return View(course);
                }

                if (_courseManager.IsCourseCodeExist(course.CourseCode))
                {
                    FlashMessage.Danger("Course Code Already Exist");
                    return View(course);
                }

                if (_courseManager.SaveCourses(course))
                {
                    FlashMessage.Confirmation("Course Saved Successfully");
                    return RedirectToAction("Create");
                }
            }

            FlashMessage.Danger("Some error occured,Please Check all the input field");
            return View();
        }

        public ActionResult AssignCourse()
        {
            var departments = _departmentManager.GetAllDepartments();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        public ActionResult AssignCourse(CourseAssign courseAssign)
        {
            var departments = _departmentManager.GetAllDepartments();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");

            if (ModelState.IsValid)
            {
                if (_courseManager.IsCourseAssigned(courseAssign.CourseId))
                {
                    FlashMessage.Danger("Course already been assigned");
                    return View();
                }

                if (_courseManager.AssignCourse(courseAssign))
                {
                    FlashMessage.Confirmation("Course assigned successfully");
                    return View();
                }
            }
            FlashMessage.Danger("Some error occured; please check all the inputs ");
            return View();
        }

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

        public JsonResult GetTeacherByDepartment(int departmentId)
        {
            var teachers = _teacherManager.GetTeacherWithDepartmentId(departmentId);
            return Json(teachers);
        }

        public JsonResult GetTeacherById(int teacherId)
        {
            var teacher = _teacherManager.GetAllTeachers().FirstOrDefault(t => t.Id == teacherId);
            return Json(teacher);
        }

        public JsonResult GetTeacherRemainingCredit(int teacherId)
        {
            double remainingCredit = _teacherManager.TeacherRemainingCredit(teacherId);

            return Json(remainingCredit);
        }

        public JsonResult GetAllCoursesByDepartmentId(int departmentId)
        {
            var courses = _courseManager.GetAllCoursesByDepartmentId(departmentId);
            return Json(courses);
        }

        public JsonResult GetCourseById(int courseId)
        {
            var course = _courseManager.GetAllCourses().FirstOrDefault(c => c.Id == courseId);
            return Json(course);
        }

        public JsonResult IsCourseAssigned(int courseid)
        {
            bool isAssigned = _courseManager.IsCourseAssigned(courseid);
            if (isAssigned)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        private void FillDepartmentAndSemesterDropdown()
        {
            var departments = _departmentManager.GetAllDepartments();
            var semesters = _courseManager.GetAllSemester();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");
            ViewBag.SemisterId = new SelectList(semesters, "SemisterId", "SemisterName");
        }
    }


}