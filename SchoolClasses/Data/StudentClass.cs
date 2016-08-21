using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolClasses.Data
{
    public class StudentClass
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ClassId { get; set; }
        public float GPA { get; set; }
    }
}