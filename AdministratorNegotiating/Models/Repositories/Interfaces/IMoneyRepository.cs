using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministratorNegotiating.Models.Repositories.Interfaces
{
    public interface IMoneyRepository
    {
        decimal GetMoney(string UserName);

        bool RemoveMoney(string UserName, decimal Count);

        bool AddMoney(string UserName, decimal Count);
    }
}