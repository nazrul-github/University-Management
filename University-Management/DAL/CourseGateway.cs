﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                var courses = _projectDbContext.Courses.OrderBy(c=>c.Id).ToList();
                return courses;
            }
        }
        public List<Course> GetAllCoursesByDepartmentId(int departmentId)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var courses = _projectDbContext.Courses.Where(c => c.DepartmentId == departmentId).OrderBy(c=>c.Id).ToList();
                return courses;
            }
        }

        public List<Semester> GetAllSemester()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var semesters = _projectDbContext.Semisters.OrderBy(s=>s.SemesterId).ToList();
                return semesters;
            }
        }

        public List<TeacherCourseAssign> GetAllAssignedCourse()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var courseAssigned = _projectDbContext.TeacherCourseAssigns.ToList();
                return courseAssigned;
            }
        }

        public bool AssignCourse(TeacherCourseAssign teacherCourseAssign)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                _projectDbContext.TeacherCourseAssigns.Add(teacherCourseAssign);
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
            using (_projectDbContext = new ProjectDbContext())
            {
                var departmentAssignCourse = _projectDbContext.TeacherCourseAssigns.Include(c => c.Teacher)
                    .Where(c => c.DepartmentId == departmentId & c.IsAssigned).OrderBy(c => c.CourseId).ToList();
                var departCourse = _projectDbContext.Courses.Include(c => c.Semester).Where(c => c.DepartmentId == departmentId).OrderBy(c => c.Id).ToList();
                var assignedCourses = departCourse.Join(departmentAssignCourse, c => c.Id, a => a.CourseId, (course, assign) => new
                {
                    CourseId = course.Id,
                    CourseCode = course.CourseCode,
                    Name = course.CourseName,
                    Semester = course.Semester.SemesterName,
                    AssignedTo = assign.Teacher.Name
                }).ToList();

                var courses = departCourse.Select(c => new
                {
                    CourseId = c.Id,
                    CourseCode = c.CourseCode,
                    Name = c.CourseName,
                    Semester = c.Semester.SemesterName,
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
               
                var courseStatics = new List<CourseTeacherView>();

                foreach (var c in allCourses)
                {
                    
                    courseStatics.Add(new CourseTeacherView
                    {
                        AssignedTo = c.AssignedTo,
                        CourseCode = c.CourseCode,
                        Name = c.Name,
                        Semester = c.Semester
                    });
                }
                
                return courseStatics;
            }
        }

        public List<StudentCourseAssign> GetAllStudentCourses()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var studentCourses = _projectDbContext.StudentCourseAssigns.Include(c => c.Course).Where(c=>c.IsAssigned).ToList();
                return studentCourses;
            }
        }

        public bool UnAssignAllCourse()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var teacherCourses = _projectDbContext.TeacherCourseAssigns.Where(t=>t.IsAssigned).ToList();
                var studentCourses = _projectDbContext.StudentCourseAssigns.Where(s=>s.IsAssigned).ToList();
                var studentResult = _projectDbContext.Results.Where(r=>r.IsResultAvailable).ToList();

                foreach (var course in teacherCourses)
                {
                    course.IsAssigned = false;
                }

                foreach (var course in studentCourses)
                {
                    course.IsAssigned = false;
                }

                foreach (var result in studentResult)
                {
                    result.IsResultAvailable = false;
                }
                _projectDbContext.SaveChanges();
                return true;
            }
        }
    }
}

