using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using University_Management.Models;
using University_Management.ViewModel;

namespace University_Management.DAL
{
    public class CourseGateway
    {
        private ProjectDbContext _projectDbContext;

        public bool CreateCourse(Course course)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                _projectDbContext.Courses.Add(course);
                _projectDbContext.SaveChanges();
                int affected = course.Id;

                if (affected > 0)
                {
                    return true;
                }

                return false;
            }
        }

        public List<Course> GetAllCourses()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var courses = _projectDbContext.Courses.ToList();
                return courses;
            }
        }
        public List<Course> GetAllCoursesByDepartmentId(int departmentId)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var courses = _projectDbContext.Courses.Where(c => c.DepartmentId == departmentId).ToList();
                return courses;
            }
        }

        public List<Semister> GetAllSemester()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var semesters = _projectDbContext.Semisters.ToList();
                return semesters;
            }
        }

        public List<CourseAssign> GetAllAssignedCourse()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var courseAssigned = _projectDbContext.CourseAssigns.ToList();
                return courseAssigned;
            }
        }

        public bool AssignCourse(CourseAssign courseAssign)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                _projectDbContext.CourseAssigns.Add(courseAssign);
                _projectDbContext.SaveChanges();
                int id = courseAssign.Id;
                if (id > 0)
                {
                    return true;
                }

                return false;
            }
        }

        public List<CourseTeacherView> GetCourseStatics(int departmentId)
        {
            //var courseAssign = _projectDbContext.CourseAssigns.Include(c => c.Course).Include(c => c.Teacher).Where(c => c.DepartmentId == departmentId).ToList();
            //var course = _projectDbContext.Courses.Where(c => c.DepartmentId == departmentId).ToList();
            //courseAssign.Select(c => new
            //{
            //    CourseCode = c.Course.CourseCode,
            //    Name
            //});
            using (_projectDbContext = new ProjectDbContext())
            {
                var departmentAssignCourse = _projectDbContext.CourseAssigns.Include(c => c.Teacher)
                    .Where(c => c.DepartmentId == departmentId & c.IsAssigned).ToList();
                var departCourse = _projectDbContext.Courses.Include(c=>c.Semister).Where(c => c.DepartmentId == departmentId).ToList();

                var coursesTeacher = new List<CourseTeacherView>();

                foreach (var course in departCourse)
                {

                    foreach (var courseassign in departmentAssignCourse)
                    {
                        if (courseassign.CourseId == course.Id)
                        {

                            var courseTeacher = new CourseTeacherView()
                            {
                                CourseCode = course.CourseCode,
                                Name = course.CourseName,
                                Semester = course.Semister.SemisterName,
                                AssignedTo = courseassign.Teacher.Name
                            };
                            coursesTeacher.Add(courseTeacher);
                        }
                        else
                        {
                            var courseTeacher = new CourseTeacherView()
                            {
                                CourseCode = course.CourseCode,
                                Name = course.CourseName,
                                Semester = course.Semister.SemisterName,
                                AssignedTo = "Not Assigned Yet"
                            };
                            coursesTeacher.Add(courseTeacher);
                        }
                    }

                };
                return coursesTeacher;
            }
        }
           
    }
}

