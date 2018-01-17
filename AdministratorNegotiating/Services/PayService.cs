using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdministratorNegotiating.Models.Repositories.Interfaces;

namespace AdministratorNegotiating.Services
{
    public class PayService
    {
        private IMoneyRepository _moneyRepository;
        private IMeetingsRepository _meetingsRepository;

        public PayService(IMoneyRepository moneyRepository, IMeetingsRepository meetingsRepository)
        {
            _moneyRepository = moneyRepository;
            _meetingsRepository = meetingsRepository;
        }

        public bool MakePay(int meetingId, string userName)
        {
            int price = _meetingsRepository.GetById(meetingId).Price;
            string meetingUserName = _meetingsRepository.GetById(meetingId).UserName;
            if (meetingUserName != userName)
            {
                return false;
            }
            try
            {
                if (_moneyRepository.RemoveMoney(userName, price))
                {
                    _meetingsRepository.Pay(meetingId);
                }
                else
                {
                    throw new Exception("Not enough money");
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}