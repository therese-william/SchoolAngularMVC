using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolClasses.Data
{
    public class ClassDetails
    {
        public int Id { get; set; }
        public string StudentFullName { get; set; }
        public int Age { get; set; }
        public float GPA { get; set; }
    }
}