using SchoolClasses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolClasses.Controllers
{
    public class ClassesController : ApiController
    {
        public ISchoolRepository _repo;
        public ClassesController(ISchoolRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Class> Get()
        {
            return _repo.GetClasses().Take(50);
        }
        public HttpResponseMessage Get(int id)
        {
            var oldClass = _repo.GetClasses().Where(c => c.Id == id).ToList().FirstOrDefault();

            if (oldClass != null) return Request.CreateResponse(HttpStatusCode.OK, oldClass);

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
        public HttpResponseMessage Post([FromBody]Class newClass)
        {
            if (_repo.AddClass(newClass) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, newClass);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        public HttpResponseMessage Put([FromBody]Class updatedClass)
        {
            if (_repo.UpdateClass(updatedClass))
            {
                return Request.CreateResponse(HttpStatusCode.Accepted, updatedClass);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        public HttpResponseMessage Delete([FromUri]int classId)
        {
            if (_repo.DeleteClass(classId) && _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Accepted);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
