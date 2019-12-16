using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using University_Management.BLL;
using University_Management.Models;
using Vereyon.Web;

namespace University_Management.Controllers
{
    public class ResultController : Controller
    {
        private readonly ResultManager _resultManager = new ResultManager();
        private readonly StudentManager _studentManager = new StudentManager();
        private readonly CourseManager _courseManager = new CourseManager();


        public ActionResult AddResult()
        {
            FillStudentDropDown();
            return View();
        }

        [HttpPost]
        public ActionResult AddResult(Result result)
        {
            FillStudentDropDown();
            if (ModelState.IsValid)
            {
                if (_resultManager.AddResult(result))
                {
                    FlashMessage.Confirmation("Result added successfully");
                    return RedirectToAction("AddResult");
                }
            }
            FlashMessage.Danger("Some error occured, please check all the input");
            return View();
        }

        public ActionResult ViewResult()
        {
            FillStudentDropDown();
            return View();
        }

        public ActionResult GetCourseByStudentId(int studentId)
        {
            var courses = _courseManager.GetAllStudentCourses().Where(c => c.StudentId == studentId).Select(c => new
            {
                CourseId = c.Course.Id,
                CourseName = c.Course.CourseName
            }).ToList();

            return Json(courses);
        }

        public JsonResult GetStudentResult(int studentId)
        {
            var studentPublishedResult = _resultManager.GetAllStudentResults().Where(r => r.StudentId == studentId).Select(r => new
            {
                CourseId = r.CourseId,
                CourseCode = r.Course.CourseCode,
                CourseName = r.Course.CourseName,
                Grade = r.Grade
            }).ToList();

            var studentAllCourse = _courseManager.GetAllStudentCourses().Where(c => c.StudentId == studentId).Select(c => new
            {
                CourseId = c.Course.Id,
                CourseName = c.Course.CourseName,
                CourseCode = c.Course.CourseCode
            }).ToList();

            var studentCourseWithoutResultId = studentAllCourse.Select(c => new {CourseId = c.CourseId})
                .Except(studentPublishedResult.Select(p => new {CourseId = p.CourseId})).ToList();

            var studentCourseWithoutResult = studentCourseWithoutResultId.Join(studentAllCourse, c => c.CourseId,
                a => a.CourseId, (ci, ac) => new
                {

                    CourseId = ac.CourseId,
                    CourseCode = ac.CourseCode,
                    CourseName = ac.CourseName,
                    Grade = "Not Graded yet"
                });
            var studentResult = studentPublishedResult.Union(studentCourseWithoutResult).ToList();

            return Json(studentResult);
        }

        private void FillStudentDropDown()
        {
            var students = _studentManager.GetAllStudents();
            ViewBag.StudentId = new SelectList(students, "Id", "RegistrationNumber");
        }



    }
}