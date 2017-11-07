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
    public class GetMeetingsTimesApiController : ApiController
    {
        private IUserManagerRepository _udb;
        private IMeetingsRepository _mdb;

        public GetMeetingsTimesApiController(IUserManagerRepository udb, IMeetingsRepository mbd)
        {
            _udb = udb;
            _mdb = mbd;
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
    }
}
