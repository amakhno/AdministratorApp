using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorNegotiating.Models.Repositories.Interfaces
{
    public interface IMeetingsRepository
    {
        bool isAllow(int id, DateTime begin, DateTime end);
        Meeting GetById(int id);
        List<Meeting> ListOfWaitingMeetings();
        void Confirm(int id);
    }
}
