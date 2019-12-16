using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using University_Management.BLL;
using University_Management.Models;
using Vereyon.Web;

namespace University_Management.Controllers
{
    public class ClassRoomController : Controller
    {
        readonly ClassRoomManager _classRoomManager = new ClassRoomManager();
        readonly DepartmentManager _departmentManager = new DepartmentManager();
        readonly CourseManager _courseManager = new CourseManager();

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

        public ActionResult ClassAllocationAndRoomView()
        {
            FillDepartmentDropdown();
            return View();
        }

        public ActionResult UnAllocateClassRoom()
        {
            return View();
        }

        public JsonResult GetCourseByDepartment(int id)
        {
            var courses = _courseManager.GetAllCourses().Where(c => c.DepartmentId == id).ToList();
            return Json(courses);
        }

        public JsonResult GetAllocatedClassroomsByDepartment(int departmentId)
        {
            var classroom = _classRoomManager.GetAllocatedClassroomsByDepartment(departmentId);
            var allocatedCourse = classroom.Select(c => c.Course).Distinct().Select(a=>new
            {
                CourseId = a.Id
            }).ToList();
            var courses = _courseManager.GetAllCourses().Where(c => c.DepartmentId == departmentId).ToList();
            var allocatedClassroomInfo = allocatedCourse.Join(courses, a => a.CourseId, c => c.Id,(ac,cls)=>new
            {
                CourseId = ac.CourseId,
                CourseName = cls.CourseName,
                CourseCode = cls.CourseCode,
                ScheduleInfo = Scheduleinfo(departmentId, ac.CourseId).ToString()
            } ).ToList();

            var unallocatedCourses = courses.Select(c => new{CourseId = c.Id}).Except(allocatedClassroomInfo.Select(c => new {CourseId = c.CourseId})).Select(c=>new
            {
                CourseId = c.CourseId
            });
            var unallocatedCoursesInfo = unallocatedCourses.Join(courses, u => u.CourseId, c => c.Id, (u, ua) => new
            {
                CourseId = ua.Id,
                CourseName = ua.CourseName,
                CourseCode = ua.CourseCode,
                ScheduleInfo = "Not Scheduled Yet"
            });

            var allocatedClassroom = unallocatedCoursesInfo.Union(allocatedClassroomInfo).OrderBy(c=>c.CourseId).ToList();

            return Json(allocatedClassroom);
        }

        public JsonResult UnAllocateClassRooms()
        {
            bool IsUnAllocated = _classRoomManager.UnAllocateClassRooms();
            return Json(IsUnAllocated);
        }

        private StringBuilder Scheduleinfo(int departmentId, int courseId)
        {
            var classroom = _classRoomManager.GetAllocatedClassroomsByDepartment(departmentId);
            var allocatedClassroom = classroom.Where(c => c.CourseId == courseId).ToList();
            var sheduleString = new StringBuilder();

            foreach (var Room in classroom)
            {
                sheduleString.Append(
                    $"R.No: {Room.Room.RoomNo}, {Room.Day.Substring(0, 3)}, {Room.FromTime.ToShortTimeString()} - {Room.ToTime.ToShortTimeString()}<br>");
            }

            return sheduleString;
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