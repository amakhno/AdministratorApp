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
    public class GetUserMeetingsApiController : ApiController
    {
        private IUserManagerRepository _udb;
        private IMeetingsRepository _mdb;

        public GetUserMeetingsApiController(IUserManagerRepository udb, IMeetingsRepository mbd)
        {
            _udb = udb;
            _mdb = mbd;
        }

        // GET: api/MyMeetingsApi/5
        public string[] Get(string username, string password)
        {
            if (_udb.Login(username, password))
            {
                return _mdb.GetByUserName(username);
            }
            else
            {
                return new string[] { "error" };
            }
        }
    }
}
