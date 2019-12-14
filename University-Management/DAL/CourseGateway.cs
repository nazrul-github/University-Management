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

        public List<TeacherCourseAssign> GetAllAssignedCourse()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var courseAssigned = _projectDbContext.CourseAssigns.ToList();
                return courseAssigned;
            }
        }

        public bool AssignCourse(TeacherCourseAssign teacherCourseAssign)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                _projectDbContext.CourseAssigns.Add(teacherCourseAssign);
                _projectDbContext.SaveChanges();
                int id = teacherCourseAssign.Id;
                if (id > 0)
                {
                    return true;
                }

                return false;
            }
        }

        public List<CourseTeacherView> GetCourseStatics(int departmentId)
        {
            //var teacherCourseAssign = _projectDbContext.CourseAssigns.Include(c => c.Course).Include(c => c.Teacher).Where(c => c.DepartmentId == departmentId).ToList();
            //var course = _projectDbContext.Courses.Where(c => c.DepartmentId == departmentId).ToList();
            //teacherCourseAssign.Select(c => new
            //{
            //    CourseCode = c.Course.CourseCode,
            //    Name
            //});
            using (_projectDbContext = new ProjectDbContext())
            {
                var departmentAssignCourse = _projectDbContext.CourseAssigns.Include(c => c.Teacher)
                    .Where(c => c.DepartmentId == departmentId & c.IsAssigned).OrderBy(c => c.CourseId).ToList();
                var departCourse = _projectDbContext.Courses.Include(c => c.Semister).Where(c => c.DepartmentId == departmentId).OrderBy(c => c.Id).ToList();
                var assignedCourses = departCourse.Join(departmentAssignCourse, c => c.Id, a => a.CourseId, (course, assign) => new
                {
                    CourseId = course.Id,
                    CourseCode = course.CourseCode,
                    Name = course.CourseName,
                    Semester = course.Semister.SemisterName,
                    AssignedTo = assign.Teacher.Name
                }).ToList();

                var courses = departCourse.Select(c => new
                {
                    CourseId = c.Id,
                    CourseCode = c.CourseCode,
                    Name = c.CourseName,
                    Semester = c.Semister.SemisterName,
                    AssignedTo = "Not Assigned yet"
                }).ToList();

                var courseCodes = courses.Select(x=>new{x.CourseId}).Except(assignedCourses.Select(x=>new{x.CourseId}))
                    .ToList();
                
                var notAssignedCourses = courses.Join(courseCodes,c=>c.CourseId,cc=>cc.CourseId,(ano,a)=>new
                {
                    CourseId = ano.CourseId,
                    CourseCode = ano.CourseCode,
                    Name = ano.Name,
                    Semester = ano.Semester,
                    AssignedTo = ano.AssignedTo
                });

                var allCourses = assignedCourses.Select(x => new
                {
                    x.CourseId,
                    x.Name,
                    x.CourseCode,
                    x.Semester,
                    x.AssignedTo
                }).Union(notAssignedCourses.Select(x => new
                {
                    x.CourseId,
                    x.Name,
                    x.CourseCode,
                    x.Semester,
                    x.AssignedTo
                })).Select(c => new
                {
                    CourseId = c.CourseId,
                    Name = c.Name,
                    CourseCode = c.CourseCode,
                    Semester = c.Semester,
                    c.AssignedTo
                });
               
                var assignedcourses = new List<CourseTeacherView>();

                foreach (var c in allCourses)
                {
                    
                    assignedcourses.Add(new CourseTeacherView
                    {
                        AssignedTo = c.AssignedTo,
                        CourseCode = c.CourseCode,
                        Name = c.Name,
                        Semester = c.Semester
                    });
                }
                


                //foreach (var course in departCourse)
                //  {

                //      foreach (var courseassign in departmentAssignCourse)
                //      {
                //          if (courseassign.CourseId == course.Id)
                //          {

                //              var courseTeacher = new CourseTeacherView()
                //              {
                //                  CourseCode = course.CourseCode,
                //                  Name = course.CourseName,
                //                  Semester = course.Semister.SemisterName,
                //                  AssignedTo = courseassign.Teacher.Name
                //              };
                //              coursesTeacher.Add(courseTeacher);
                //          }
                //          else
                //          {
                //              var courseTeacher = new CourseTeacherView()
                //              {
                //                  CourseCode = course.CourseCode,
                //                  Name = course.CourseName,
                //                  Semester = course.Semister.SemisterName,
                //                  AssignedTo = "Not Assigned Yet"
                //              };
                //              coursesTeacher.Add(courseTeacher);
                //          }
                //          break;
                //      }

                //  };
                return assignedcourses;
            }
        }

    }
}

