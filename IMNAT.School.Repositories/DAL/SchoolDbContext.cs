﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using IMNAT.School.Models.Entities.DomainModels;

namespace IMNAT.School.Repositories.DAL
{
    public class SchoolDbContext: DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options): base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
