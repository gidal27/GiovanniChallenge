using Db_Context;
using ViewModel;
using IMNAT.School.Services.Services.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel;
using static IMNAT.School.Services.Services.Configurations.StartUp;

namespace IMNAT.School.Services.Services.Implementations
{
    public class SchoolManagement
    {

        readonly SchoolDbContext _SchoolDbContext;

        /// <summary>
        /// Class Service called in main for doing all required Tasks...
        /// </summary>
        /// <param name="SchoolDbContext"></param>
        /// 

        public SchoolManagement(SchoolDbContext SchoolDbContext) => _SchoolDbContext = SchoolDbContext;

        /// <summary>
        /// fill dataTable Course with some initial values....
        /// </summary>
        public void SeedDatabase()
        {
            var table = _SchoolDbContext.Courses;
            string[] InitialValues = FileConfigRead.InitialContent; // a surveiller durant le temps d'execution
            Course InitialSingleContent = new Course();
            List<Course> InitialContent = new List<Course>();


            if (table.CountAsync().Result == 0) // we check if there  is already some initial values...
            {
                for (int i = 0; i < InitialValues.Length; i++)
                {

                    InitialContent.Add(new Course { Name = InitialValues[i] });
                };

                try
                {
                    foreach (Course course in InitialContent)
                    {
                        _SchoolDbContext.Courses.Add(course);

                    }
                    _SchoolDbContext.SaveChanges();
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }

            }
        }


        /// <summary>
        /// Get all courses in database
        /// </summary>
        /// <returns>Courses </returns>
        public IEnumerable<Course> GetAllCourses()
        {


            var ListCourses = _SchoolDbContext.Courses;

            return ListCourses;
        }
        /// <summary>
        /// Create a student in database..
        /// </summary>
        /// <param name="StudentName"></param>
        /// <param name="email"></param>
        /// <returns>string</returns>

        public string CreateStudent(string StudentName, string email)
        {
            string[] Names = StudentName.Split(' ');
            Student student = new Student();

            if (Names.Length > 2)
            {
                for (int i = 0; i < (Names.Length - 1); i++)
                { student.FirstName += (Names[i] + " "); }

                student.LastName = Names[(Names.Length - 1)];
                student.Email = email;
            }

            else
            {
                student.FirstName = Names[0]; student.LastName = Names[1];
                student.Email = email;
            }

            _SchoolDbContext.Students.Add(student);

            _SchoolDbContext.SaveChanges();
            return (student.LastName);
        }

        /// <summary>
        /// Make the junction between a student and a course selected
        /// </summary>
        /// <param name="StudentName"></param>
        /// <param name="CourseID"></param>
        public void LinkStudentToCourse(string StudentName, int CourseID)
        {

            StudentCourse Selection = new StudentCourse { SelectedCourseID = CourseID, Student = StudentName };

            try
            {
                _SchoolDbContext.SelectedCourses.Add(Selection);
                _SchoolDbContext.SaveChanges();
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
        }

        /// <summary>
        /// Display Selected all course selected
        /// </summary>
        /// <returns></returns>

        public Dictionary<string, IEnumerable<string>> DisplaySelection()
        {

            Dictionary<string, IEnumerable<string>> AllSelections = new Dictionary<string, IEnumerable<string>>();

            var RegisteredStudents = from StudentNames in _SchoolDbContext.Students.AsEnumerable()
                                     select StudentNames.LastName;

            foreach (string key in RegisteredStudents)
            {
                var CourseNames = from c in _SchoolDbContext.Courses
                                  join s in _SchoolDbContext.SelectedCourses on c.Id equals s.SelectedCourseID
                                  where s.Student == key
                                  select c.Name;

                AllSelections.Add(key, CourseNames);

            }

            return AllSelections;

        }

    }
}
