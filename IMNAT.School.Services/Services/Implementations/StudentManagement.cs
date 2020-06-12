using System;
using System.Collections.Generic;
using System.Text;
using IMNAT.School.Repositories.DAL.Repository;
using IMNAT.School.Repositories.DAL.Repository.Implementations;

namespace IMNAT.School.Services.Services.Implementations
{
    public class StudentManagement : IStudentManagement
    {
        readonly IStudentrepo _StudentRepo;

        public StudentManagement(IStudentrepo StudentRepo)
        {
            _StudentRepo = StudentRepo;

        }

        public void CreateStudent(string studentName, string email)
        {
            _StudentRepo.CreateStudent(studentName, email);
        }
    }
}
