using AdministratorNegotiating.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdministratorNegotiating.Controllers.Api
{
    public class MoneyController : ApiController
    {
        private IUserManagerRepository _udb;
        private IMoneyRepository _moneyRepository;

        public MoneyController(IUserManagerRepository mdb, IMoneyRepository moneyRepository)
        {
            _udb = mdb;
            _moneyRepository = moneyRepository;
        }

        [Authorize]
        public string Get()
        {
            return _moneyRepository.GetMoney(User.Identity.Name).ToString();
        }

        [Authorize]
        [HttpPost]
        [AcceptVerbs("POST")]
        public string Post([FromBody] string value)
        {
            //var value = request.Content.ReadAsAsync<string>().Result;
            return _moneyRepository.AddMoney(User.Identity.Name, Convert.ToDecimal(value)).ToString();
        }
    }
}
