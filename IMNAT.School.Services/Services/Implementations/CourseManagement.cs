using System;
using System.Collections.Generic;
using System.Text;
using Db_Context;
using IMNAT.School.Repositories.DAL.Repository;
using IMNAT.School.Repositories.DAL.Repository.Implementations;
using IMNAT.School.Services.Services;

namespace IMNAT.School.Services.Services.Implementations
{
    public class CourseManagement : ICourseManagement
    {
         readonly ICoursesRepo _coursesRepo;

        public CourseManagement(ICoursesRepo coursesRepo)
        {
            _coursesRepo = coursesRepo;

        }

        public IEnumerable<Course> DisplayAvailableCourses() {

            var ListCourses = _coursesRepo.GetAllCourses();
         
                return ListCourses;
            }

        

        public void LinkStudentToCourse() {
        
        }

        //public void DisplaySelectedCourses() { 
        
        //}
    }
}
