using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace AdministratorNegotiating.Models.Repositories
{
    public class Repository
    {
        protected void Run(Action<ApplicationDbContext> dbAction)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                dbAction(db);
            }
        }

        protected void RunUser(Action<UserManager<ApplicationUser>> dbAction)
        {
            using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                dbAction(userManager);
            }
        }

        protected bool RunBool(Func<ApplicationDbContext, bool> dbAction)
        {
            bool result;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                result = dbAction(db);
            }
            return result;
        }
    }
}