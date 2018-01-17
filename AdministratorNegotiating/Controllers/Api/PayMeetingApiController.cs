using AdministratorNegotiating.Models.Repositories.Interfaces;
using AdministratorNegotiating.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AdministratorNegotiating.Controllers
{
    public class PayMeetingApiController : ApiController
    {
        private IMeetingsRepository _meetingsRepository;
        private IMoneyRepository _moneyRepository;

        public PayMeetingApiController(IMeetingsRepository meetingsRepository, IMoneyRepository moneyRepository)
        {
            _meetingsRepository = meetingsRepository;
            _moneyRepository = moneyRepository;
        }

        [Authorize]
        public bool Post([FromBody]int meetingId)
        {
            PayService payService = new PayService(_moneyRepository, _meetingsRepository);
            if (payService.MakePay(meetingId, User.Identity.Name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
