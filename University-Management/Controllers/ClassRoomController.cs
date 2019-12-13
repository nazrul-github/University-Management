using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University_Management.BLL;

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
            FillCourseDropdown();
            FillRoomDropdown();
            return View();
        }
        

        private void FillDepartmentDropdown()
        {
            var departments = _departmentManager.GetAllDepartments();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");
        }
       

        private void FillCourseDropdown()
        {
            var courses = _courseManager.GetAllCourses();
            ViewBag.CourseId = new SelectList(courses, "Id", "CourseName");
        }
        private void FillRoomDropdown()
        {
            var room = _classRoomManager.GetAllRoomInfo();
            ViewBag.RoomId = new SelectList(room, "RoomId", "RoomNo");
        }
    }
}