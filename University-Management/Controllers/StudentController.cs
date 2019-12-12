using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University_Management.BLL;
using University_Management.Models;

namespace University_Management.Controllers
{
    public class StudentController : Controller
    {
        readonly StudentManager _studentManager = new StudentManager();
        readonly DepartmentManager _departmentManager = new DepartmentManager();
        TeacherManager _teacherManager = new TeacherManager();
        public ActionResult Create()
        {
            FillDepartmentDropdown();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            return RedirectToAction("Create");
        }

        public ActionResult ShowStudent(Student student)
        {
            return View(student);
        }
        public JsonResult CreateStudent(int id)
        {
            if (ModelState.IsValid)
            {
                //if (_teacherManager.IsEmailExist(student.Email))
                //{
                //    return Json("Email exist in the database");
                //}

                //var year = student.Date.ToString("yyyy");
                //var depCode = _departmentManager.GetAllDepartments()
                //    .FirstOrDefault(d => d.DepartmentId == student.DepartmentId)?.DepartmentCode;
                //int id = _studentManager.AddStudent(student);
                //var idNumber = id.ToString("000");
                //var combined = $"{depCode}-{year}-{idNumber}";
                //bool isUpdated = _studentManager.UpdateStudent(id, combined);
                //student.RegistrationNumber = combined;
                //var msg = $"Student Name: {student.StudentName}\n" +
                //          $"Student Email: {student.Email}\n" +
                //          $"Student Contact no: {student.ContactNo}\n" +
                //          $"Registration Date: {student.Date:dd-MMM-yy}\n" +
                //          $"Address: {student.Address}\n" +
                //          $"Department: Computer Science\n" +
                //          $"Registration number: {combined}";
              //  return Json(msg);
            }
            var studentDb = _studentManager.GetAllStudents().FirstOrDefault(s => s.Id == id);
            return Json(studentDb,JsonRequestBehavior.AllowGet);
            // return Json("Please fill the form properly");
        }

        private void FillDepartmentDropdown()
        {
            var departments = _departmentManager.GetAllDepartments();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");
        }
    }
}