using AdministratorNegotiating.Models.Repositories.Interfaces;
using Microsoft.AspNet.Identity;
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
            PasswordHasher a = new PasswordHasher();
           
            bool result = RunBool((context) => 
            {
                if (context.Users.Where(x => x.UserName == name).Count() == 0)
                {
                    return false;
                }
                var user = context.Users.First(x=>x.UserName == name);
                if (a.VerifyHashedPassword(user.PasswordHash, hashPassword) == PasswordVerificationResult.Success)
                {
                    return true;                    
                }
                return false;
            });
            return result;
        }
    }
}