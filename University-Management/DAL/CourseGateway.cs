using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_Management.Models;

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

                if (affected>0)
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

        public List<Semister> GetAllSemester()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var semesters = _projectDbContext.Semisters.ToList();
                return semesters;
            }
        }
    }
}