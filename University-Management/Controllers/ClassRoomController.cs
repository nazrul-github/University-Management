using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University_Management.BLL;
using University_Management.Models;
using Vereyon.Web;

namespace University_Management.Controllers
{
    public class ClassRoomController : Controller
    {
      ClassRoomManager _classRoomManager = new ClassRoomManager();
      DepartmentManager _departmentManager = new DepartmentManager();
      CourseManager _courseManager = new CourseManager();

        public ActionResult AllocateClassRoom()
        {
            FillDepartmentDropdown();
          //  FillCourseDropdown();
            FillRoomDropdown();
            return View();
        }

        [HttpPost]
        public ActionResult AllocateClassRoom(AllocateClassroom classroom)
        {
            FillDepartmentDropdown();
            FillRoomDropdown();
            if (ModelState.IsValid)
            {
                if (classroom.FromTime>=classroom.ToTime)
                {
                    FlashMessage.Danger("From time should be less than to time");
                    ModelState.Clear();
                    return View(classroom);
                }
                if (!_classRoomManager.IsClassRoomAvailable(classroom.Day,classroom.FromTime,classroom.ToTime))
                {
                    FlashMessage.Danger($"Class room is not available between {classroom.FromTime.ToShortTimeString()} and {classroom.ToTime.ToShortTimeString()} on {classroom.Day}");
                    ModelState.Clear();
                    return View(classroom);
                }
                _classRoomManager.SaveAllocatedClassRoom(classroom);
                FlashMessage.Confirmation("Class room successfully allocated");
                return RedirectToAction("AllocateClassRoom");
            }
            FlashMessage.Danger("Some error occured, please check all the input");
            return View(classroom);
        }

        public JsonResult GetCourseByDepartment(int id)
        {
            var courses = _courseManager.GetAllCourses().Where(c => c.DepartmentId == id).ToList();
            return Json(courses);
        }

        private void FillDepartmentDropdown()
        {
            var departments = _departmentManager.GetAllDepartments();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");
        }
       

        //private void FillCourseDropdown()
        //{
        //    var courses = _courseManager.GetAllCourses();
        //    ViewBag.CourseId = new SelectList(courses, "Id", "CourseName");
        //}
        private void FillRoomDropdown()
        {
            var room = _classRoomManager.GetAllRoomInfo();
            ViewBag.RoomId = new SelectList(room, "RoomId", "RoomNo");
        }

        
    }
}