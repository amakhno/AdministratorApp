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
    public class MeetingsRepository : Repository , IMeetingsRepository
    {
        public IEnumerable<Meeting> TakeAll()
        {
            IEnumerable<Meeting> meetings = null;
            RunWithUpdateStatuses(contex =>
            {
                meetings = contex.Meetings.OrderByDescending(x => x.DayOfBooking).ToArray();
            });
            return meetings;
        }

        public bool isAllow(int id, DateTime begin, DateTime end)
        {
            bool result = false;
            RunWithUpdateStatuses(contex =>
            {
                if (begin >= end)
                {
                    result = false;
                    return;
                }

                if (contex.Meetings.Where(x => x.MeetingRoomId == id) == null)
                {
                    result = true;
                    return;
                }

                foreach (Meeting meeting in contex.Meetings.Where(x => x.MeetingRoomId == id).
                    Where(x => ((x.Status != Meeting.StatusTypes.Rejected) && (x.Status != Meeting.StatusTypes.Ended))))
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
            RunWithUpdateStatuses(contex =>
            {
                result = contex.Meetings.Find(id);
            });
            return result;
        }

        public List<Meeting> ListOfWaitingMeetings()
        {
            List<Meeting> list = new List<Meeting>();
            RunWithUpdateStatuses(contex =>
            {
                list = contex.Meetings.Include(m => m.MeetingRoom).Where(x => x.Status == Meeting.StatusTypes.Waiting)
                    .OrderByDescending(x => x.DayOfBooking).ToList();
            });
            return list;
        }

        public List<Meeting> ListOfAcrhive()
        {
            List<Meeting> list = new List<Meeting>();
            RunWithUpdateStatuses(contex =>
            {
                list = contex.Meetings.Include(m => m.MeetingRoom).Where(x => (x.Status == Meeting.StatusTypes.Rejected || x.Status == Meeting.StatusTypes.Ended)).
                    OrderByDescending(x => x.DayOfBooking).ToList();
            });
            return list;
        }

        public List<Meeting> ListOfInProcess()
        {
            List<Meeting> list = new List<Meeting>();
            RunWithUpdateStatuses(contex =>
            {
                list = contex.Meetings.Include(m => m.MeetingRoom).Where(x => (x.Status == Meeting.StatusTypes.Confirmed || x.Status == Meeting.StatusTypes.Paid))
                    .OrderByDescending(x => x.DayOfBooking).ToList();
            });
            return list;
        }

        public void Confirm(int id)
        {
            RunWithUpdateStatuses(contex =>
            {
                var meeting = contex.Meetings.Find(id);
                meeting.Status = Meeting.StatusTypes.Confirmed;
                contex.Entry(meeting).State = EntityState.Modified;
                contex.SaveChanges();
            });
        }

        public void Reject(int id)
        {
            RunWithUpdateStatuses(contex =>
            {
                var meeting = contex.Meetings.Find(id);
                meeting.Status = Meeting.StatusTypes.Rejected;
                contex.Entry(meeting).State = EntityState.Modified;
                contex.SaveChanges();
            });
        }

        public void DeleteById(int id)
        {
            RunWithUpdateStatuses(contex =>
            {
                Meeting meeting = contex.Meetings.Find(id);
                contex.Meetings.Remove(meeting);
                contex.SaveChanges();
            });
        }

        public void Add(Meeting meeting)
        {
            RunWithUpdateStatuses(contex =>
            {
                contex.Meetings.Add(meeting);
                contex.SaveChanges();
            });
        }    

        public string[] GetTimeInfo(int id)
        {
            List<string> stringResult = new List<string>();
            RunWithUpdateStatuses(context =>
            {
                foreach (Meeting mr in context.Meetings.Where(x => x.MeetingRoomId == id).Where(x => x.Status != Meeting.StatusTypes.Ended)
                    .Where(x => x.Status != Meeting.StatusTypes.Rejected).OrderByDescending(x => x.DayOfBooking))
                {
                    stringResult.Add(mr.NameOfMeeting);
                    stringResult.Add(mr.BeginTime.ToString());
                    stringResult.Add(mr.EndTime.ToString());
                }
            });
            return stringResult.ToArray();
        }

        public string[] GetByUserName(string username)
        {
            List<string> stringResult = new List<string>();
            RunWithUpdateStatuses(context =>
            {
                foreach (Meeting mr in context.Meetings.Where(x => x.UserName == username).Where(x => x.Status != Meeting.StatusTypes.Ended)
                    .Where(x => x.Status != Meeting.StatusTypes.Rejected).Include(x => x.MeetingRoom).OrderByDescending(x => x.DayOfBooking))
                {
                    stringResult.Add(mr.Id.ToString());
                    stringResult.Add(mr.NameOfMeeting);
                    stringResult.Add(mr.MeetingRoom.Name);
                    stringResult.Add(mr.BeginTime.ToString());//ToString("MM.dd.yyyy HH:mm") + "|";
                    stringResult.Add(mr.EndTime.ToString());//.ToString("MM.dd.yyyy HH:mm") + "|";
                    stringResult.Add(mr.Status.ToString());
                    stringResult.Add(mr.Price.ToString());
                }
            });
            return stringResult.ToArray();
        }

        public void DeleteAllMeetingsWithRoom(int roomId)
        {
            Run(context =>
            {
                foreach (Meeting a in context.Meetings.Where(x => x.MeetingRoomId == roomId))
                {
                    context.Meetings.Remove(a);
                }
                context.SaveChanges();
            });
        }

        public void Pay(int meetingId)
        {
            Run(context =>
                {
                    context.Meetings.First(x => x.Id == meetingId).Status = Meeting.StatusTypes.Paid;
                    context.SaveChanges();
                });
        }
    }
}