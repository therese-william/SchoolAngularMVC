using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolClasses.Data
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public ICollection<StudentClass> StudentClasses { get; set; }
    }
}