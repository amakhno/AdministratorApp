using AdministratorNegotiating.Models.Repositories.Interfaces;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;


namespace AdministratorNegotiating.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginApiController : ApiController
    {
        private IUserManagerRepository _udb;

        public LoginApiController(IUserManagerRepository mdb)
        {
            _udb = mdb;
        }

        // GET: api/CustomApi
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET: api/CustomApi/5
        [HttpPost]
        public bool Ask(string username, string password)
        {
            return _udb.Login(username, password);
        }

        // GET: api/LoginApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/LoginApi/5
        public string Get(int id)
        {
            return "value";
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        // POST: api/LoginApi
        public bool Post(string username, string password)
        {
            return _udb.Login(username, password);
        }

        // PUT: api/LoginApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LoginApi/5
        public void Delete(int id)
        {
        }
    }
}
