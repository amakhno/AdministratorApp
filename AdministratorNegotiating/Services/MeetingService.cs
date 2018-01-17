using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdministratorNegotiating.Models;
using AdministratorNegotiating.Models.Repositories.Interfaces;

namespace AdministratorNegotiating.Services
{
    public class MeetingService
    {
        private IMoneyRepository _moneyRepository;
        private IMeetingsRepository _meetingsRepository;

        public MeetingService(IMoneyRepository moneyRepository, IMeetingsRepository meetingsRepository)
        {
            _moneyRepository = moneyRepository;
            _meetingsRepository = meetingsRepository;
        }

        public void Delete(int Id)
        {
            string userName = _meetingsRepository.GetById(Id).UserName;
            if (_meetingsRepository.GetById(Id).Status == Meeting.StatusTypes.Paid)
            {
                _moneyRepository.AddMoney(userName, _meetingsRepository.GetById(Id).Price);
            }
            _meetingsRepository.DeleteById(Id);
        }
    }
}