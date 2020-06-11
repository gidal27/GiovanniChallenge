using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace Db_Context
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Student> Students;

    }
}
