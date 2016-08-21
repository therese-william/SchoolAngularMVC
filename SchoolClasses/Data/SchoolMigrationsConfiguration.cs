using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace SchoolClasses.Data
{
    public class SchoolMigrationsConfiguration : DbMigrationsConfiguration<SchoolContext>
    {
        public SchoolMigrationsConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SchoolContext context)
        {
            base.Seed(context);

#if DEBUG
            if (context.Students.Count() == 0)
            {
                var student1 = new Student { 
                    FirstName="David",
                    LastName="Jackson",
                    Age=19
                };

                var student2 = new Student
                {
                    FirstName = "Peter",
                    LastName = "Parker",
                    Age = 19
                };

                var student3 = new Student
                {
                    FirstName = "Robert",
                    LastName = "Smith",
                    Age = 18
                };

                var student4 = new Student
                {
                    FirstName = "Rebecca",
                    LastName = "Black",
                    Age = 19
                };
                context.Students.Add(student1);
                context.Students.Add(student2);
                context.Students.Add(student3);
                context.Students.Add(student4);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
            if (context.Classes.Count() == 0)
            {
                var myclass = new Class { 
                    ClassName = "Biology",
                    Location = "Building 5 Room 201",
                    TeacherName = "Mr Robertson"
                };
                context.Classes.Add(myclass);
                var myotherclass = new Class
                {
                    ClassName = "English",
                    Location = "Building 3 Room 134",
                    TeacherName = "Miss Sanderson"
                };
                context.Classes.Add(myotherclass);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
            if (context.StudentClasses.Count() == 0)
            {
                var studentclass1 = new StudentClass
                {
                    ClassId=1,
                    StudentId=1,
                    GPA=3.4F
                };
                var studentclass2 = new StudentClass
                {
                    ClassId = 1,
                    StudentId = 2,
                    GPA = 2.9F
                };
                var studentclass3 = new StudentClass
                {
                    ClassId = 1,
                    StudentId = 3,
                    GPA = 3.1F
                };
                var studentclass4 = new StudentClass
                {
                    ClassId = 1,
                    StudentId = 4,
                    GPA = 2.1F
                };
                context.StudentClasses.Add(studentclass1);
                context.StudentClasses.Add(studentclass2);
                context.StudentClasses.Add(studentclass3);
                context.StudentClasses.Add(studentclass4);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
#endif
        }
    }
}
