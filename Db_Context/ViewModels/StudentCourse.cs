using System;
using System.Collections.Generic;
using System.Text;

namespace Db_Context.ViewModels
{
    public class StudentCourse
    {
        public int ID { get; set; }
        public int SelectedCourseID {get; set;} // Options of each course
        public string Student { get; set; } // for student name


    }
}
