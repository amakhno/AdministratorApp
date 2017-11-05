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

        public List<Meeting> ListOfAcrhive()
        {
            List<Meeting> list = new List<Meeting>();
            Run(contex =>
            {
                list = contex.Meetings.Include(m => m.MeetingRoom).Where(x => (x.Status == Meeting.StatusTypes.Rejected || x.Status == Meeting.StatusTypes.Ended)).
                    OrderByDescending(x=>x.DayOfBooking).ToList();
            });
            return list;
        }

        public List<Meeting> ListOfInProcess()
        {
            List<Meeting> list = new List<Meeting>();
            Run(contex =>
            {
                list = contex.Meetings.Include(m => m.MeetingRoom).Where(x => (x.Status == Meeting.StatusTypes.Confirmed)).ToList();
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

        public void Reject(int id)
        {
            Run(contex =>
            {
                var meeting = contex.Meetings.Find(id);
                meeting.Status = Meeting.StatusTypes.Rejected;
                contex.Entry(meeting).State = EntityState.Modified;
                contex.SaveChanges();
            });
        }

        public void UpdateStatuses()
        {
            Run(contex =>
            {
                var meetings = contex.Meetings.Where(x => (x.EndTime < DateTime.Now && x.Status != Meeting.StatusTypes.Rejected));
                foreach (Meeting meeting in meetings)
                {
                    meeting.Status = Meeting.StatusTypes.Ended;
                    contex.Entry(meeting).State = EntityState.Modified;
                }
                contex.SaveChanges();
            });
        }

        public void DeleteById(int id)
        {
            Run(contex =>
            {
                Meeting meeting = contex.Meetings.Find(id);
                contex.Meetings.Remove(meeting);
                contex.SaveChanges();
            });
        }

        public void Add(Meeting meeting)
        {
            Run(contex => 
            {
                contex.Meetings.Add(meeting);
                contex.SaveChanges();
            });
        }
    }
}