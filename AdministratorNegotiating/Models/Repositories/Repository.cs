﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;

namespace AdministratorNegotiating.Models.Repositories
{
    public class Repository
    {
        protected void RunWithUpdateStatuses(Action<ApplicationDbContext> dbAction)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                UpdateStatuses(db);
                dbAction(db);
            }
        }

        protected void Run(Action<ApplicationDbContext> dbAction)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                dbAction(db);
            }
        }

        private void UpdateStatuses(ApplicationDbContext contex)
        {
                var meetings = contex.Meetings.Where(x => (x.EndTime < DateTime.Now && x.Status != Meeting.StatusTypes.Rejected));
                foreach (Meeting meeting in meetings)
                {
                    meeting.Status = Meeting.StatusTypes.Ended;
                    contex.Entry(meeting).State = EntityState.Modified;
                }
                contex.SaveChanges();
        }

        protected void RunUser(Action<UserManager<ApplicationUser>> dbAction)
        {
            using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
            {
                dbAction(userManager);
            }
        }
    }
}