using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministratorNegotiating.Models
{
    public class MeetingTableUserPosition
    {
        public int Id;
        public string Name;
        public int CountOfChairs;
        public bool IsProjector;
        public bool IsBoard;
        public DateTime firstMeetingDate;
    }
}