using Db_Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMNAT.School.Repositories.DAL.Repository
{
   public interface IStudentrepo
    {
        void CreateStudent(string StudentName, string email);
        IEnumerable<Student> GetAllStudents(); //gewt all students form database
    }
}
