using System;
using System.Collections.Generic;
using System.Text;
using Db_Context;
using IMNAT.School.Repositories.DAL.Repository.Implementations;

namespace IMNAT.School.Services.Services
{
    public interface ICourseManagement
    {
        IEnumerable<Course> DisplayAvailableCourses(); // Display on the console all available courses...
        void LinkStudentToCourse(); // let student choose which course he want to be in...

        //List<> DisplaySelectedCourses(); // display all courses which have been selected by student...
    }
}
