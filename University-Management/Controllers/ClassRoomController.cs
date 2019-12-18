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
                if (classroom.FromTime >= classroom.ToTime)
                {
                    FlashMessage.Danger("From time should be less than to time");
                    ModelState.Clear();
                    return View(classroom);
                }
                if (!_classRoomManager.IsClassRoomAvailable(classroom.Day, classroom.FromTime, classroom.ToTime))
                {
                    FlashMessage.Danger($"Class room is not available between {classroom.FromTime.ToShortTimeString()} to {classroom.ToTime.ToShortTimeString()} on {classroom.Day}");
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

        public ActionResult ClassAllocationAndRoomView()
        {
            FillDepartmentDropdown();
            return View();
        }

        public JsonResult GetAllocatedClassroomsByDepartment(int departmentId)
        {
            //all the isAllocated classroom for this department
            var classroom = _classRoomManager.GetAllocatedClassroomsByDepartment(departmentId);
            //As class room allocation is having all the course id we are selecting the distinct course id from them
            var allocatedCourse = classroom.Select(c => c.Course).Distinct().Select(a => new
            {
                CourseId = a.Id // if i select (c => c.Course.Id) anonymous object creation not possible
            }).ToList();
            //Bringing all the courses by department id
            var courses = _courseManager.GetAllCourses().Where(c => c.DepartmentId == departmentId).ToList();
            //After bringing all the course id only joining the distinct courseId we have from class room allocation tbl
            var allocatedClassroomInfo = allocatedCourse.Join(courses, a => a.CourseId, c => c.Id, (ac, cls) => new
            {
                CourseId = ac.CourseId,
                CourseName = cls.CourseName,
                CourseCode = cls.CourseCode,
                ScheduleInfo = ScheduleInfo(departmentId, ac.CourseId).ToString()//sending department id, and distinct course id for schedule info
            }).ToList();

            //From that department's all the courses we are selecting rest of the courses id except the id's from clsrm alctn tbl
            var unallocatedCourses = courses.Select(c => new { CourseId = c.Id }).Except(allocatedClassroomInfo.Select(c => new { CourseId = c.CourseId })).Select(c => new
            {
                CourseId = c.CourseId
            });
            //Joining those id with the course tbl, and setting the schedule info as not scheduled
            var unallocatedCoursesInfo = unallocatedCourses.Join(courses, u => u.CourseId, c => c.Id, (u, ua) => new
            {
                CourseId = ua.Id,
                CourseName = ua.CourseName,
                CourseCode = ua.CourseCode,
                ScheduleInfo = "Not Scheduled Yet"
            });

            // Union between alctd classroom courseId and all the other course id and their properties
            var allocatedClassroom = unallocatedCoursesInfo.Union(allocatedClassroomInfo).OrderBy(c => c.CourseId).ToList();

            return Json(allocatedClassroom);
        }

        private StringBuilder ScheduleInfo(int departmentId, int courseId)
        {
            //brought all the allocated clsrm
            var classroom = _classRoomManager.GetAllocatedClassroomsByDepartment(departmentId);
            // from that clsrms only slct the clsrm with the course id
            var allocatedClassroom = classroom.Where(c => c.CourseId == courseId).ToList();
            //building string with info
            var scheduleString = new StringBuilder();
            var counter = 1;
            foreach (var room in allocatedClassroom)
            {
                scheduleString.Append(
                    $"R.No: {room.Room.RoomNo}, {room.Day.Substring(0, 3)}, {room.FromTime.ToShortTimeString()} - {room.ToTime.ToShortTimeString()}");
                if (counter<allocatedClassroom.Count)
                {
                    scheduleString.Append(";<br>");
                }

                counter++;
            }
            return scheduleString;
        }

        public ActionResult UnAllocateClassRoom()
        {
            return View();
        }

        public JsonResult UnAllocateClassRooms()
        {
            bool isUnAllocated = _classRoomManager.UnAllocateClassRooms();
            return Json(isUnAllocated);
        }

        private void FillDepartmentDropdown()
        {
            var departments = _departmentManager.GetAllDepartments();
            ViewBag.DepartmentId = new SelectList(departments, "DepartmentId", "DepartmentName");
        }

        private void FillRoomDropdown()
        {
            var room = _classRoomManager.GetAllRoomInfo();
            ViewBag.RoomId = new SelectList(room, "RoomId", "RoomNo");
        }



    }
}