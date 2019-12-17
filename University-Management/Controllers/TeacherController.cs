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
        private readonly TeacherManager _teacherManager = new TeacherManager();
        private readonly DepartmentManager _departmentManager = new DepartmentManager();
        private readonly CourseManager _courseManager = new CourseManager();

        public ActionResult Create()
        {
            FillDesignationDropDown();
            FillDepartmentDropdown();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            FillDesignationDropDown();
            FillDepartmentDropdown();

            if (ModelState.IsValid)
            {
                if (_teacherManager.IsEmailExist(teacher.Email))
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

        public ActionResult AssignCourse()
        {
            FillDepartmentDropdown();
            return View();
        }

        [HttpPost]
        public ActionResult AssignCourse(TeacherCourseAssign teacherCourseAssign)
        {
            FillDepartmentDropdown();
            if (ModelState.IsValid)
            {
                if (_courseManager.IsCourseAssigned(teacherCourseAssign.CourseId))
                {
                    FlashMessage.Danger("Course already been assigned");
                    return RedirectToAction("AssignCourse");
                }

                if (_courseManager.AssignCourse(teacherCourseAssign))
                {
                    FlashMessage.Confirmation("Course assigned successfully");
                    return RedirectToAction("AssignCourse");
                }
            }
            FlashMessage.Danger("Some error occured; please check all the inputs ");
            return View();
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

        public JsonResult IsEmailExist(string email)
        {
            bool isExist = _teacherManager.IsEmailExist(email);

            if (isExist)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //Fill the dropdown methods
        private void FillDepartmentDropdown()
        {
            var departments = _departmentManager.GetAllDepartments();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");
        }

        private void FillDesignationDropDown()
        {
            var designations = _teacherManager.GetAllDesignations();
            ViewBag.DesignationId = new SelectList(designations, "Id", "DesignationName");
        }
    }
}