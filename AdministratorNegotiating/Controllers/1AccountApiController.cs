using AdministratorNegotiating.Models.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdministratorNegotiating.Controllers
{
    public class AccountApiController : ApiController
    {
        private IUserManagerRepository _udb;      

        public AccountApiController(IUserManagerRepository userManager)
        {
            _udb = userManager;
        }        

        // GET: api/AccountApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AccountApi/5
        public bool Get(string UserName, string Password)
        {
            PasswordHasher hasher = new PasswordHasher();
            Password = hasher.HashPassword(Password);
            if (_udb.Login(UserName, Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // POST: api/AccountApi
        public bool Post(string UserName, string Password)
        {
            PasswordHasher hasher = new PasswordHasher();
            Password = hasher.HashPassword(Password);
            if (_udb.Login(UserName, Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // PUT: api/AccountApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/AccountApi/5
        public void Delete(int id)
        {
        }
    }
}
