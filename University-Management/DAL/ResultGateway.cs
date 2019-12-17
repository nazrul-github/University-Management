using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using University_Management.Models;

namespace University_Management.DAL
{
    public class ResultGateway
    {
        private ProjectDbContext _projectDbContext;

        public bool AddResult(Result result)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                _projectDbContext.Results.Add(result);
                _projectDbContext.SaveChanges();
                int id = result.Id;
                if (id > 0)
                {
                    return true;
                }

                return false;
            }
        }

        public List<Result> GetAllStudentResults()
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var finalResults = _projectDbContext.Results.Include(r => r.Course).Where(r => r.IsResultAvailable).ToList();

                return finalResults;
            }
        }

        public bool IsResultExistForThisStudent(int resultCourseId, int resultDepartmentId, int resultStudentId)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                bool isExist = _projectDbContext.Results.Any(r =>
                    r.CourseId == resultCourseId && r.DepartmentId == resultDepartmentId &&
                    r.StudentId == resultStudentId && r.IsResultAvailable);
                return isExist;
            }
        }

        public bool UpdateResult(Result result)
        {
            using (_projectDbContext = new ProjectDbContext())
            {
                var resultDb = _projectDbContext.Results.FirstOrDefault(r =>
                    r.CourseId == result.CourseId && r.DepartmentId == result.DepartmentId &&
                    r.StudentId == result.StudentId && r.IsResultAvailable);
                resultDb.Grade = result.Grade;
                int affected = _projectDbContext.SaveChanges();
                if (affected > 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}