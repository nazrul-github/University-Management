using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Rotativa.MVC;
using University_Management.BLL;
using University_Management.Models;
using University_Management.ViewModel;
using Vereyon.Web;

namespace University_Management.Controllers
{
    [Authorize(Roles = "robin")]
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
                if (_resultManager.IsResultExistForThisStudent(result.CourseId,result.DepartmentId,result.StudentId))
                {
                    if (_resultManager.UpdateResult(result))
                    {
                        FlashMessage.Confirmation("Result updated successfully");
                        return RedirectToAction("AddResult");
                    }
                }
                if (_resultManager.AddResult(result))
                {
                    FlashMessage.Confirmation("Result added successfully");
                    return RedirectToAction("AddResult");
                }
                FlashMessage.Danger("Some error occured, please try again later");
                return View(result);
            }
            FlashMessage.Danger("Some error occured, please check all the input");
            return View();
        }

        public ActionResult ViewResult()
        {
            FillStudentDropDown();
            return View();
        }

        public ActionResult PdfResult(int studentId)
        {
            var studentResult = StudentResult(studentId);

            return View(studentResult);
        }

        public JsonResult GetCourseByStudentId(int studentId)
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
            var studentResult = StudentResult(studentId);

            //sending the list of result course view
            return Json(studentResult.ResultCourseViews);
        }



        private StudentResultViewModel StudentResult(int studentId)
        {
            //Student With Id
            var aStudent = _studentManager.GetAllStudents().FirstOrDefault(s => s.Id == studentId);
            //Brought the student course grade form results table
            var studentPublishedResult = _resultManager.GetAllStudentResults().Where(r => r.StudentId == studentId).Select(r =>
                new
                {
                    CourseId = r.CourseId,
                    CourseCode = r.Course.CourseCode,
                    CourseName = r.Course.CourseName,
                    Grade = r.Grade
                }).ToList();

            //Brougnt all the courses assigned to that student
            var studentAllCourse = _courseManager.GetAllStudentCourses().Where(c => c.StudentId == studentId).Select(c => new
            {
                CourseId = c.Course.Id,
                CourseName = c.Course.CourseName,
                CourseCode = c.Course.CourseCode
            }).ToList();

            //Selecting not published results course id from course table
            var studentCourseWithoutResultId = studentAllCourse.Select(c => new {CourseId = c.CourseId})
                .Except(studentPublishedResult.Select(p => new {CourseId = p.CourseId})).ToList();

            //Joining not published results course id with all course to get the course code course name and assiging grad
            var studentCourseWithoutResult = studentCourseWithoutResultId.Join(studentAllCourse, c => c.CourseId,
                a => a.CourseId, (ci, ac) => new
                {
                    CourseId = ac.CourseId,
                    CourseCode = ac.CourseCode,
                    CourseName = ac.CourseName,
                    Grade = "Not Graded yet"
                });
            //Made union with not publised result with not published result
            var studentResult = studentPublishedResult.Union(studentCourseWithoutResult).ToList();
            var resultCourses = new List<ResultCourseView>();
            //with foreach loop adding the course grade to result course view models results view
            foreach (var result in studentResult)
            {
                resultCourses.Add(new ResultCourseView()
                {
                    CourseCode = result.CourseCode,
                    CourseName = result.CourseName,
                    Grade =  result.Grade
                });
            }
            //populating the object of student result view model for the pdf
            var studentResultView = new StudentResultViewModel()
            {
                StudentDepartment = aStudent.Department.DepartmentName,
                StudentName = aStudent.StudentName,
                StudentEmail = aStudent.Email,
                StudentRegistrationNumber = aStudent.RegistrationNumber,
                ResultCourseViews = resultCourses
            };

            return studentResultView;
        }

        public JsonResult IsResultExist(int courseId, int departmentId, int studentId)
        {
            bool isExist = _resultManager.IsResultExistForThisStudent(courseId, departmentId, studentId);

            if (isExist)
            {
                return Json(false);
            }

            return Json(true);

        }

        private void FillStudentDropDown()
        {
            var students = _studentManager.GetAllStudents();
            ViewBag.StudentId = new SelectList(students, "Id", "RegistrationNumber");
        }


    }
}