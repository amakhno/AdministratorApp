using AdministratorNegotiating.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AdministratorNegotiating.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TimeApiController : ApiController
    {
        private IUserManagerRepository _udb;
        private IMeetingsRepository _mdb;

        public TimeApiController(IUserManagerRepository udb, IMeetingsRepository mbd)
        {
            _udb = udb;
            _mdb = mbd;
        }
        // GET: api/TimeApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TimeApi/5
        public string Get(int id, string username, string password)
        {
            if (_udb.Login(username, password))
            {
                return _mdb.GetTimeInfo(id);
            }
            else
            {
                return "error";
            }
        }

        // POST: api/TimeApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TimeApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TimeApi/5
        public void Delete(int id)
        {
        }
    }
}
