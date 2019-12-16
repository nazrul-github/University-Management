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
                var results = _projectDbContext.Results.Include(r => r.Course).ToList();
                return results;
            }
        }
    }
}