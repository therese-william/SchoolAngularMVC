using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolClasses.Data
{
    public class SchoolRepository : ISchoolRepository
    {
        SchoolContext _ctx;
        public SchoolRepository(SchoolContext ctx)
        {
            _ctx = ctx;
        }
        
        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Policy");
                return false;
            }
        }
        
        public IQueryable<Class> GetClasses()
        {
            return _ctx.Classes;
        }

        public IQueryable<StudentClass> GetStudentsInClass(int ClassId)
        {
            return _ctx.StudentClasses.Where(sc => sc.ClassId == ClassId);
        }
        
        public IQueryable<ClassDetails> GetClassDetails(int ClassId)
        {
            var studentclasses = GetStudentsInClass(ClassId).ToList();
            List<ClassDetails> result = new List<ClassDetails>();
            foreach (var sc in studentclasses)
            {
                var student = _ctx.Students.Where(s => s.Id == sc.StudentId).ToList().FirstOrDefault();
                var cd = new ClassDetails { 
                    Id = sc.Id,
                    StudentFullName = student.FirstName + " " + student.LastName,
                    Age = student.Age,
                    GPA = sc.GPA
                };
                result.Add(cd);
            }
            return result.AsQueryable();
        }

        public bool AddClass(Class newClass)
        {
            try
            {
                _ctx.Classes.Add(newClass);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Policy");
                return false;
            }
        }

        public bool UpdateClass(Class updatedClass)
        {
            try
            {
                var oldClass = _ctx.Classes.Where(c => c.Id == updatedClass.Id).ToList().FirstOrDefault();
                if (oldClass != null)
                {
                    oldClass.ClassName = updatedClass.ClassName;
                    oldClass.Location = updatedClass.Location;
                    oldClass.TeacherName = updatedClass.TeacherName;
                    return Save();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Policy");
                return false;
            }
        }

        public bool DeleteClass(int classId)
        {
            try
            {
                var classToDelete = _ctx.Classes.Where(c => c.Id == classId).ToList().FirstOrDefault();
                _ctx.Classes.Remove(classToDelete);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Policy");
                return false;
            }
        }

        public IQueryable<Student> GetStudents()
        {
            return _ctx.Students;
        }

        public bool AddStudent(Student newStudent)
        {
            try
            {
                _ctx.Students.Add(newStudent);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Policy");
                return false;
            }
        }




        public bool AddStudentClass(StudentClass studentClass)
        {
            _ctx.StudentClasses.Add(studentClass);
            return true;
        }

        public IQueryable<StudentClass> GetStudentClasses()
        {
            return _ctx.StudentClasses;
        }


        public IQueryable<Student> GetFreeStudents(int classId, int StudentId)
        {
            var currentClassStudentIds = _ctx.StudentClasses.Where(s => s.ClassId == classId).Select(s => s.StudentId).ToList();
            currentClassStudentIds.Remove(StudentId);
            var currentClassStudentLastNames = _ctx.Students.Where(s => currentClassStudentIds.Contains(s.Id)).Select(s => s.LastName).ToList();
            return _ctx.Students.Where(s => !currentClassStudentLastNames.Contains(s.LastName));
        }


        public bool DeleteStudentClass(int studentClassId)
        {
            try
            {
                var studentClassToDelete = _ctx.StudentClasses.Where(c => c.Id == studentClassId).ToList().FirstOrDefault();
                _ctx.StudentClasses.Remove(studentClassToDelete);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Policy");
                return false;
            }

        }

        public bool UpdateStudentClass(StudentClass updatedStudentClass)
        {
            try
            {
                var oldStudentClass = _ctx.StudentClasses.Where(c => c.Id == updatedStudentClass.Id).ToList().FirstOrDefault();
                if (oldStudentClass != null)
                {
                    oldStudentClass.ClassId = updatedStudentClass.ClassId;
                    oldStudentClass.GPA = updatedStudentClass.GPA;
                    oldStudentClass.StudentId = updatedStudentClass.StudentId;
                    return Save();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Log Policy");
                return false;
            }
        }
    }
}