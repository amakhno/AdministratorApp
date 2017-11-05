using AdministratorNegotiating.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministratorNegotiating.Models.Repositories
{
    public class UserManagerRepository : Repository, IUserManagerRepository
    {
        public bool Login(string name, string hashPassword)
        {
            bool result = false;
            Run(context => 
            {
                if (context.Users.Where(x => x.UserName == name).Count() == 0)
                {
                    result = false;
                    return;
                }
                var user = context.Users.First(x=>x.UserName == name);
                if (user.PasswordHash == hashPassword)
                {
                    result = true;
                    return;
                }
            });
            return result;
        }
    }
}