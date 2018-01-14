using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdministratorNegotiating.Models
{
    public class Meeting2
    {
        public Meeting2(MeetingAdminViewModel viewModel)
        {
            //if (viewModel.BeginTime)
        }

        public int Id { get; protected set; }

        public DateTime DayOfBooking { get; protected set; }

        public string NameOfMeeting { get; protected set; }

        public DateTime BeginTime { get; protected set; }

        public DateTime EndTime { get; protected set; }

        public int MeetingRoomId { get; protected set; }

        public MeetingRoom MeetingRoom { get; protected set; }

        // Ссылка на покупателя
        public virtual ApplicationUser User { get; protected set; }

        public string UserName { get; protected set; }

        public StatusTypes Status { get; protected set; }

        public enum StatusTypes
        {
            Confirmed,
            Rejected,
            Ended,
            Waiting
        }
    }
}