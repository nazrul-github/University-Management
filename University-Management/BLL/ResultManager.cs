using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using University_Management.DAL;
using University_Management.Models;

namespace University_Management.BLL
{
    public class ResultManager
    {
        ResultGateway _resultGateway = new ResultGateway();

        public bool AddResult(Result result)
        {
            return _resultGateway.AddResult(result);
        }

        public List<Result> GetAllStudentResults()
        {
            return _resultGateway.GetAllStudentResults();
        }
    }
}