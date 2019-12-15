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
                student.RegistrationNumber = combined;
                var msg = $"Student Name: {student.StudentName};   " +
                          $"Student Email: {student.Email};  " +
                          $"Student Contact no: {student.ContactNo};    " +
                          $"Registration Date: {student.Date:dd-MMM-yy};    " +
                          $"Address: {student.Address};     " +
                          $"Department: {department.DepartmentName};     " +
                          $"Registration number: {combined}";
                FlashMessage.Confirmation(msg);
                // ViewBag.Message = msg;
                // ModelState.Clear();
                //return View();
                return RedirectToAction("Create");
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
                    FlashMessage.Danger("Student Already Assigned To This Course");
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

        public JsonResult GetCourseByDepartment(int departmentId)
        {
            var courses = _courseManager.GetAllCourses().Where(c => c.DepartmentId == departmentId).ToList();
            return Json(courses);
        }

        //public JsonResult CreateStudent(int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //if (_teacherManager.IsEmailExist(student.Email))
        //        //{
        //        //    return Json("Email exist in the database");
        //        //}

        //        //var year = student.Date.ToString("yyyy");
        //        //var depCode = _departmentManager.GetAllDepartments()
        //        //    .FirstOrDefault(d => d.DepartmentId == student.DepartmentId)?.DepartmentCode;
        //        //int id = _studentManager.AddStudent(student);
        //        //var idNumber = id.ToString("000");
        //        //var combined = $"{depCode}-{year}-{idNumber}";
        //        //bool isUpdated = _studentManager.UpdateStudent(id, combined);
        //        //student.RegistrationNumber = combined;
        //        //var msg = $"Student Name: {student.StudentName}\n" +
        //        //          $"Student Email: {student.Email}\n" +
        //        //          $"Student Contact no: {student.ContactNo}\n" +
        //        //          $"Registration Date: {student.Date:dd-MMM-yy}\n" +
        //        //          $"Address: {student.Address}\n" +
        //        //          $"Department: Computer Science\n" +
        //        //          $"Registration number: {combined}";
        //      //  return Json(msg);
        //    }
        //    var studentDb = _studentManager.GetAllStudents().FirstOrDefault(s => s.Id == id);
        //    return Json(studentDb,JsonRequestBehavior.AllowGet);
        //    // return Json("Please fill the form properly");
        //}

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
    }
}