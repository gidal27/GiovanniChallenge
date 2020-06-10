using System;
using System.Collections.Generic;
using System.Text;
using IMNAT.School.Repositories.DAL.Repository.Implementations;

namespace IMNAT.School.Services.Services
{
    public interface ICourseManagement
    {
        void SeedCourses(StudentsRepo studentRepo);


    }
}
