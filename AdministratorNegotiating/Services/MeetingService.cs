using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdministratorNegotiating.Models;

namespace AdministratorNegotiating.Services
{
    public class MeetingService
    {
        public AddOrUpdateMeeting(MeetingAdminViewModel viewModel)
        {
            if (viewModel.Id == 0)
            {
                Meeting2 meeting = new Meeting2(viewModel);
            }
        }
    }
}