﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IMNAT.School.Models.Entities.DomainModels
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Student> Students;

    }
}
