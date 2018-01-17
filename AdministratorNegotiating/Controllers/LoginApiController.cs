using AdministratorNegotiating.Models.Repositories.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;


namespace AdministratorNegotiating.Controllers
{
    public class LoginApiController : ApiController
    {
        private IUserManagerRepository _udb;

        public LoginApiController(IUserManagerRepository mdb)
        {
            _udb = mdb;
            
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // POST: api/LoginApi
        public bool Post(string username, string password)
        {
            return _udb.Login(username, password);
        }
    }
}
