using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministratorNegotiating.Models.Repositories
{
    public class MeetRepository : IMeetRepository
    {
        private ApplicationDbContext contex = new ApplicationDbContext();
        private ApplicationUserManager _manager;

        public MeetRepository(ApplicationUserManager userManager)
        {
            _manager = userManager;
        }

        public List<Meeting> GetListOfMeetingsByUserId(int userId)
        {
            
            return new List<Meeting>();
        }
    }
}