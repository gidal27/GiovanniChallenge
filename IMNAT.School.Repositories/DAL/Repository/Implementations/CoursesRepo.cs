using Db_Context;
using IMNAT.School.Repositories.DAL.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;


namespace IMNAT.School.Repositories.DAL
{
     public class CoursesRepo : ICoursesRepo
    {
       readonly SchoolDbContext _context;      

        public CoursesRepo(SchoolDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAllCourses() {


            var ListCourses = _context.Courses;

                return ListCourses;
            }
            
        }

    }
