using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolClasses.Data
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Location { get; set; }
        public string TeacherName { get; set; }
        public ICollection<StudentClass> StudentClasses { get; set; }
    }
}
