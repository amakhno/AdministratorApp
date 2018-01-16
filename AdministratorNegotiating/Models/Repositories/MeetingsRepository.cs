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
                meetings = contex.Meetings.OrderByDescending(x=>x.DayOfBooking).ToArray();
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
                list = contex.Meetings.Include(m => m.MeetingRoom).Where(x => x.Status == Meeting.StatusTypes.Waiting )
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
                    OrderByDescending(x=>x.DayOfBooking).ToList();
            });
            return list;
        }

        public List<Meeting> ListOfInProcess()
        {
            List<Meeting> list = new List<Meeting>();
            RunWithUpdateStatuses(contex =>
            {
                list = contex.Meetings.Include(m => m.MeetingRoom).Where(x => (x.Status == Meeting.StatusTypes.Confirmed))
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

        public string[] GetUserListInfo()
        {
            List<string> stringResult = new List<string>();
            RunWithUpdateStatuses(context =>
            {
                var result = context.MeetingRooms.ToArray();
                foreach (MeetingRoom mr in result)
                {
                    stringResult.Add(mr.Id.ToString());
                    stringResult.Add(mr.Name);
                    stringResult.Add(mr.CountOfChairs.ToString());
                    stringResult.Add(mr.IsProjector.ToString().ToLower());
                    stringResult.Add(mr.IsBoard.ToString().ToLower());
                    if (context.Meetings.Where(x => x.MeetingRoomId == mr.Id).Where(x => x.Status != Meeting.StatusTypes.Ended)
                        .Where(x => x.Status != Meeting.StatusTypes.Rejected)
                        .Count() == 0)
                    {
                        stringResult.Add("Свободна");
                    }
                    else
                    {
                        stringResult.Add(context.Meetings.Where(x => x.MeetingRoomId == mr.Id).Where(x => x.Status != Meeting.StatusTypes.Ended)
                        .Where(x => x.Status != Meeting.StatusTypes.Rejected).Min(x => x.BeginTime).ToString());
                    }
                }
            });
            return stringResult.ToArray();
        }

        public MeetingTableUserPosition[] GetUserListInfo(bool innerParam)
        {
            List<MeetingTableUserPosition> stringResult = new List<MeetingTableUserPosition>();
            RunWithUpdateStatuses(context =>
            {
                var result = context.MeetingRooms.ToArray();
                foreach (MeetingRoom mr in result)
                {
                    MeetingTableUserPosition position = new MeetingTableUserPosition();
                    position.Id = mr.Id;
                    position.Name = mr.Name;
                    position.CountOfChairs = mr.CountOfChairs;
                    position.IsProjector = mr.IsProjector;
                    position.IsBoard = mr.IsBoard;
                    if (context.Meetings.Where(x => x.MeetingRoomId == mr.Id).Where(x => x.Status != Meeting.StatusTypes.Ended)
                        .Where(x => x.Status != Meeting.StatusTypes.Rejected)
                        .Count() == 0)
                    {
                        position.firstMeetingDate = DateTime.MinValue;
                    }
                    else
                    {
                        position.firstMeetingDate = (context.Meetings.Where(x => x.MeetingRoomId == mr.Id).Where(x => x.Status != Meeting.StatusTypes.Ended)
                        .Where(x => x.Status != Meeting.StatusTypes.Rejected).Min(x => x.BeginTime));
                    }
                    stringResult.Add(position);
                }
            });
            return stringResult.ToArray();
        }

        public string[] GetTimeInfo(int id)
        {
            List<string> stringResult = new List<string>();
            RunWithUpdateStatuses(context =>
            {
                foreach (Meeting mr in context.Meetings.Where(x=>x.MeetingRoomId == id).Where(x=>x.Status != Meeting.StatusTypes.Ended)
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
                    .Where(x => x.Status != Meeting.StatusTypes.Rejected).Include(x=>x.MeetingRoom).OrderByDescending(x => x.DayOfBooking))
                {
                    stringResult.Add(mr.NameOfMeeting);
                    stringResult.Add(mr.MeetingRoom.Name);
                    stringResult.Add(mr.BeginTime.ToString());//ToString("MM.dd.yyyy HH:mm") + "|";
                    stringResult.Add(mr.EndTime.ToString());//.ToString("MM.dd.yyyy HH:mm") + "|";
                    stringResult.Add(mr.Status.ToString());
                }
            });
            return stringResult.ToArray();
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