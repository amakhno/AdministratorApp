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
    public class MyMeetingsApiController : ApiController
    {
        private IUserManagerRepository _udb;
        private IMeetingsRepository _mdb;

        public MyMeetingsApiController(IUserManagerRepository udb, IMeetingsRepository mbd)
        {
            _udb = udb;
            _mdb = mbd;
        }
        // GET: api/MyMeetingsApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MyMeetingsApi/5
        public string Get(string username, string password)
        {
            if (_udb.Login(username, password))
            {
                return _mdb.GetByUserName(username);
            }
            else
            {
                return "error";
            }
        }

        // POST: api/MyMeetingsApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MyMeetingsApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MyMeetingsApi/5
        public void Delete(int id)
        {
        }
    }
}
