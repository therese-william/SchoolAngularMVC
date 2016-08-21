using SchoolClasses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolClasses.Controllers
{
    public class StudentClassController : ApiController
    {
        private ISchoolRepository _repo;

        public StudentClassController(ISchoolRepository repo)
        {
            _repo = repo;
        }

        public HttpResponseMessage Get([FromUri]int id)
        {
            var oldStudentClass = _repo.GetStudentClasses().Where(c => c.Id == id).ToList().FirstOrDefault();

            if (oldStudentClass != null) return Request.CreateResponse(HttpStatusCode.OK, oldStudentClass);

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Post([FromBody]StudentClass updatedStudentClass)
        {
            if (_repo.AddStudentClass(updatedStudentClass) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, updatedStudentClass);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Put([FromBody]StudentClass updatedStudentClass)
        {
            if (_repo.UpdateStudentClass(updatedStudentClass))
            {
                return Request.CreateResponse(HttpStatusCode.Created, updatedStudentClass);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage Delete([FromUri]int studentClassId)
        {
            if (_repo.DeleteStudentClass(studentClassId) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
