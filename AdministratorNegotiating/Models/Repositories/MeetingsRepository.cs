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
            RunWithUpdateStatuses(contex =>
            {
                meetings = contex.Meetings.ToArray();
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
                    Where(x=>((x.Status != Meeting.StatusTypes.Rejected)&&(x.Status != Meeting.StatusTypes.Ended))))
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
                list = contex.Meetings.Include(m => m.MeetingRoom).Where(x => x.Status == Meeting.StatusTypes.Waiting ).ToList();
            });
            return list;
        }

        public List<Meeting> ListOfAcrhive()
        {
            List<Meeting> list = new List<Meeting>();
            RunWithUpdateStatuses(contex =>
            {
                list = contex.Meetings.Include(m => m.MeetingRoom).Where(x => (x.Status == Meeting.StatusTypes.Rejected || x.Status == Meeting.StatusTypes.Ended)).
                    OrderByDescending(x=>x.DayOfBooking).ToList();
            });
            return list;
        }

        public List<Meeting> ListOfInProcess()
        {
            List<Meeting> list = new List<Meeting>();
            RunWithUpdateStatuses(contex =>
            {
                list = contex.Meetings.Include(m => m.MeetingRoom).Where(x => (x.Status == Meeting.StatusTypes.Confirmed)).ToList();
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

        public string GetUserListInfo()
        {
            string stringResult = "";
            RunWithUpdateStatuses(context =>
            {
                var result = context.MeetingRooms.ToArray();
                foreach (MeetingRoom mr in result)
                {
                    stringResult += mr.Id + "|";
                    stringResult += mr.Name + "|";
                    stringResult += mr.CountOfChairs + "|";
                    stringResult += mr.IsProjector.ToString().ToLower() + "|";
                    stringResult += mr.IsBoard.ToString().ToLower() + "|";
                    if (context.Meetings.Where(x => x.MeetingRoomId == mr.Id).Where(x => x.Status != Meeting.StatusTypes.Ended)
                        .Where(x => x.Status != Meeting.StatusTypes.Rejected)
                        .Count() == 0)
                    {
                        stringResult += "free" + "|";
                    }
                    else
                    {
                        stringResult += context.Meetings.Where(x => x.MeetingRoomId == mr.Id).Where(x => x.Status != Meeting.StatusTypes.Ended)
                        .Where(x => x.Status != Meeting.StatusTypes.Rejected).Min(x => x.BeginTime).ToString("MM.dd.yyyy HH:mm") + "|";
                    }
                }
                stringResult = stringResult.Remove(stringResult.Length - 1);
            });
            return stringResult;
        }
        public string GetTimeInfo(int id)
        {
            string stringResult = "";
            RunWithUpdateStatuses(context =>
            {
                foreach (Meeting mr in context.Meetings.Where(x=>x.MeetingRoomId == id).Where(x=>x.Status != Meeting.StatusTypes.Ended).Where(x => x.Status != Meeting.StatusTypes.Rejected))
                {
                    stringResult += mr.NameOfMeeting + "|";
                    stringResult += mr.BeginTime.ToString("MM.dd.yyyy HH:mm") + "|";
                    stringResult += mr.EndTime.ToString("MM.dd.yyyy HH:mm") + "|";
                }
            });
            if (stringResult != "")
            {
                stringResult = stringResult.Remove(stringResult.Length - 1);
            }
            return stringResult;
        }

        public string GetByUserName(string username)
        {
            string stringResult = "";
            RunWithUpdateStatuses(context =>
            {
                foreach (Meeting mr in context.Meetings.Where(x => x.UserName == username).Where(x => x.Status != Meeting.StatusTypes.Ended).Where(x => x.Status != Meeting.StatusTypes.Rejected).Include(x=>x.MeetingRoom))
                {
                    stringResult += mr.NameOfMeeting + "|";
                    stringResult += mr.MeetingRoom.Name + "|";
                    stringResult += mr.BeginTime.ToString("MM.dd.yyyy HH:mm") + "|";
                    stringResult += mr.EndTime.ToString("MM.dd.yyyy HH:mm") + "|";
                    stringResult += mr.Status.ToString() + "|";
                }
            });
            if (stringResult != "")
            {
                stringResult = stringResult.Remove(stringResult.Length - 1);
            }
            return stringResult;
        }

        public List<MeetingRoom> GetAllRooms()
        {
            List<MeetingRoom> result = new List<MeetingRoom>();
            Run(context =>
            {
                result = context.MeetingRooms.ToList();
            });
            return result;
        }

        public MeetingRoom GetMeetingRoomById(int id)
        {
            MeetingRoom result = new MeetingRoom();
            Run(context =>
            {
                result = context.MeetingRooms.Find(id);
            });
            return result;
        }

        public void AddMeetingRoom(MeetingRoom meetingRoom)
        {
            Run(context =>
            {
                context.MeetingRooms.Add(meetingRoom);
                context.SaveChanges();
            });
        }

        public void UpdateMeetingRoom(MeetingRoom meetingRoom)
        {
            Run(context =>
            {
                context.Entry(meetingRoom).State = EntityState.Modified;
                context.SaveChanges();
            });
        }

        public void RemoveMeetingRoom(MeetingRoom meetingRoom)
        {
            Run(context =>
            {
                foreach(Meeting a in context.Meetings.Where(x=>x.MeetingRoomId == meetingRoom.Id))
                {
                    context.Meetings.Remove(a);
                }
                context.MeetingRooms.Remove(context.MeetingRooms.Find(meetingRoom.Id));
                context.SaveChanges();
            });
        }
    }
}