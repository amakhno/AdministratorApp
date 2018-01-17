using AdministratorNegotiating.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministratorNegotiating.Models.Repositories
{
    public class MoneyRepository : Repository, IMoneyRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public bool AddMoney(string UserName, decimal Count)
        {
            try
            {
                context.Users.First(x => x.UserName == UserName).Money += Count;
                context.SaveChanges();
            }
            catch
            {
                return false;
            }            
            return true;
        }

        public decimal GetMoney(string UserName)
        {
            decimal result = -1;
            try
            {
                result = context.Users.First(x => x.UserName == UserName).Money;
            }
            catch
            {
                ;
            }
            return result;
        }

        public bool RemoveMoney(string UserName, decimal Count)
        {
            try
            {
                context.Users.First(x => x.UserName == UserName).Money -= Count;
                context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}