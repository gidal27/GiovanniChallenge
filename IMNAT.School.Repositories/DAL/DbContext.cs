using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace Db_Context
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> SelectedCourses { get; set; }
    }
}
