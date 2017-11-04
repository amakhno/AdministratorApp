using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdministratorNegotiating.Models;
using AdministratorNegotiating.Models.Repositories.Interfaces;

namespace AdministratorNegotiating.Models.Repositories
{
    public class MeetingsRepository : Repository, IMeetingsRepository
    {
        public IEnumerable<Meeting> TakeAll()
        {
            IEnumerable<Meeting> meetings = null;
            Run(contex =>
            {
                meetings = contex.Meetings.ToArray();
            });
            return meetings;
        }

        public bool isAllow(int id, DateTime begin, DateTime end)
        {
            bool result = false;
            Run(contex =>
            {
                if (begin >= end)
                {
                    result = false;
                    return;
                }

                if (contex.Meetings.Where(x => x.Id == id) == null)
                {
                    result = true;
                    return;
                }

                foreach (Meeting meeting in contex.Meetings.Where(x => x.Id == id))
                {
                    if ((begin <= meeting.EndTime) && (end >= meeting.BeginTime))
                    {
                        result = false;
                        return;
                    }
                }

                result = true;
            });
            return result;            
        }

        public Meeting GetById(int id)
        {
            Meeting result = new Meeting();
            Run(contex =>
            {
                result = contex.Meetings.Find(id);
            });
            return result;
        }

        public List<Meeting> ListOfWaitingMeetings()
        {
            List<Meeting> list = new List<Meeting>();
            Run(contex =>
            {
                list = contex.Meetings.Include(m => m.MeetingRoom).Where(x => x.Status == Meeting.StatusTypes.Waiting ).ToList();
            });
            return list;
        }

        public void Confirm(int id)
        {
            Run(contex =>
            {
                var meeting = contex.Meetings.Find(id);
                meeting.Status = Meeting.StatusTypes.Confirmed;
                contex.Entry(meeting).State = EntityState.Modified;
                contex.SaveChanges();
            });
        }
    }
}