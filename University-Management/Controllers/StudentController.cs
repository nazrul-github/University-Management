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
    public class StudentController : Controller
    {
        readonly StudentManager _studentManager = new StudentManager();
        readonly DepartmentManager _departmentManager = new DepartmentManager();
        readonly TeacherManager _teacherManager = new TeacherManager();
        readonly CourseManager _courseManager = new CourseManager();
        public ActionResult Create()
        {
            FillDepartmentDropdown();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            FillDepartmentDropdown();

            if (ModelState.IsValid)
            {
                if (_teacherManager.IsEmailExist(student.Email))
                {
                    FlashMessage.Danger("Email exist in the database");
                    return View(student);
                }

                int id = _studentManager.AddStudent(student);
                var year = student.Date.ToString("yyyy");
                var department = _departmentManager.GetAllDepartments()
                    .FirstOrDefault(d => d.DepartmentId == student.DepartmentId);
                var idNumber = id.ToString("000");
                var combined = $"{department.DepartmentCode}-{year}-{idNumber}";
                bool isUpdated = _studentManager.UpdateStudent(id, combined);
                return RedirectToAction("Details",new{id=id});
            }
            FlashMessage.Danger("Some error occured, please check all the input");
            return View(student);
        }

        public ActionResult EnrollToCourse()
        {
            FillStudentDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult EnrollToCourse(StudentCourseAssign courseAssign)
        {
            FillStudentDropDown();
            if (ModelState.IsValid)
            {
                if (_studentManager.IsStudentAssigned(courseAssign.StudentId, courseAssign.CourseId))
                {
                    FlashMessage.Danger("This Student Already Assigned To This Course");
                    return View();
                }

                if (_studentManager.EnrollStudentToCourse(courseAssign))
                {
                    FlashMessage.Confirmation($"Student enrolled successfully");
                    return RedirectToAction("EnrollToCourse");
                }
            }
            FlashMessage.Danger("Some error occured, Please check all the input");
            return View();
        }

        public JsonResult GetStudentInfoById(int studentId)
        {
            var student = _studentManager.GetAllStudents().Find(s => s.Id == studentId);
            var department = _departmentManager.GetAllDepartments().FirstOrDefault(d => d.DepartmentId == student.DepartmentId);
            var studntDeparmtnet = new
            {
                StudentId = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Department = department.DepartmentName,
                DepartmentId = student.DepartmentId
            };
            return Json(studntDeparmtnet);
        }

        public JsonResult IsStudentAssigned(int studentId, int courseId)
        {
            if (_studentManager.IsStudentAssigned(studentId, courseId))
            {
                return Json(true);
            }

            return Json(false);
        }

        public JsonResult GetCourseByDepartment(int departmentId)
        {
            var courses = _courseManager.GetAllCourses().Where(c => c.DepartmentId == departmentId).ToList();
            return Json(courses);
        }

        private void FillDepartmentDropdown()
        {
            var departments = _departmentManager.GetAllDepartments();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");
        }

        private void FillStudentDropDown()
        {
            var students = _studentManager.GetAllStudents();
            ViewBag.StudentId = new SelectList(students, "Id", "RegistrationNumber");
        }

        public ActionResult Details(int id)
        {
            var aStudent = _studentManager.GetAllStudents().FirstOrDefault(s => s.Id == id);
            return View(aStudent);
        }

    }
}