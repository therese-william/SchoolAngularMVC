using SchoolClasses.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SchoolClasses.Controllers
{
    public class ClassDetailsController : ApiController
    {
        private ISchoolRepository _repo;
        public ClassDetailsController(ISchoolRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<ClassDetails> Get(int ClassId)
        {
            return _repo.GetClassDetails(ClassId);
        }
    }
}
