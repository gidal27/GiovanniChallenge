using Db_Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMNAT.School.Repositories.DAL.Repository.Implementations
{
    public class StudentsRepo
    {
        private SchoolDbContext _context;

        public StudentsRepo(SchoolDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetAllStudents()
        {

            return (_context.Students);
        }

        public void CreateStudent(string StudentName, string email)
        {
            string[] Names = StudentName.Split(' ');
            Student student = new Student();

            if (Names.Length > 2)
            {
                for (int i = 0; i < Names.Length; i++)
                { student.FirstName += Names[i]; }

                student.LastName = Names[(Names.Length - 1)];
                student.Email = email;
            }

            else
            {
                student.FirstName = Names[0]; student.LastName = Names[1];
                student.Email = email;
            }

            _context.Students.Add(student);

            _context.SaveChanges();
        }
    }
}
