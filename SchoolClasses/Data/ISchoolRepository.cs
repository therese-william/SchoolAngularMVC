using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolClasses.Data
{
    public interface ISchoolRepository
    {
        IQueryable<Class> GetClasses();
        IQueryable<Student> GetStudents();
        IQueryable<ClassDetails> GetClassDetails(int ClassId);
        IQueryable<StudentClass> GetStudentsInClass(int ClassId);
        bool Save();
        bool AddClass(Class newClass);
        bool AddStudent(Student newStudent);
        bool UpdateClass(Class updatedClass);
        bool DeleteClass(int classId);
        bool AddStudentClass(StudentClass studentClass);

        IQueryable<StudentClass> GetStudentClasses();

        IQueryable<Student> GetFreeStudents(int classId, int StudentId);

        bool DeleteStudentClass(int studentClassId);

        bool UpdateStudentClass(StudentClass updatedStudentClass);
    }
}
