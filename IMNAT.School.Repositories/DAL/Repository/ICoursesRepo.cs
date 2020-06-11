using Db_Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMNAT.School.Repositories.DAL.Repository
{
    public interface ICoursesRepo
    {
        IEnumerable<Course> GetAllCourses();
    }
}
