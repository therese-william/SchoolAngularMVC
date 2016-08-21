using SchoolClasses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolClasses.Controllers
{
    public class StudentsController : ApiController
    {
        public ISchoolRepository _repo;

        public StudentsController(ISchoolRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Student> Get() 
        {
            return _repo.GetStudents();
        }
        public IEnumerable<Student> Get(int classId,int StudentId)
        {
            return _repo.GetFreeStudents(classId,StudentId);
        }
        public HttpResponseMessage Post([FromBody]Student newStudent)
        {
            if (_repo.AddStudent(newStudent) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, newStudent);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
