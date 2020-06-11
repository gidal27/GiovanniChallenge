using Db_Context;
using IMNAT.School.Repositories.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;


namespace IMNAT.School.Repositories.DAL
{
     public class CoursesRepo : ICoursesRepo
    {
        private SchoolDbContext _context;

        public CoursesRepo(SchoolDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAllCourses() {

            return (_context.Courses);
        }

    }
}
