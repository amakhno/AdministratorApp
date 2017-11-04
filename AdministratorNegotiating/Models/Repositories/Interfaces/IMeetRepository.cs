using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministratorNegotiating.Models.Repositories
{
    public interface IMeetRepository
    {
        List<Meeting> GetListOfMeetingsByUserId(int id);
    }
}