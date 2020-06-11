using System;
using System.Collections.Generic;
using System.Text;
using IMNAT.School.Repositories.DAL.Repository;
using IMNAT.School.Repositories.DAL.Repository.Implementations;
using IMNAT.School.Services.Services;

namespace IMNAT.School.Services.Services.Implementations
{
    public class CourseManagement : ICourseManagement
    {
        private readonly ICoursesRepo _coursesRepo;

        public CourseManagement(ICoursesRepo coursesRepo)
        {
            _coursesRepo = coursesRepo;

        }

        public void DisplayAvailableCourses() {

            ret

        }

        public void LinkStudentToCourse() {
        
        }

        public void DisplaySelectedCourses() { 
        
        }
    }
}
