using AdministratorNegotiating.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AdministratorNegotiating.Models.Repositories
{
    public class MeetingRoomRepository : Repository, IMeetingRoomRepository
    {
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

        public void RemoveMeetingRoom(MeetingRoom meetingRoom, IMeetingsRepository meetingRepo)
        {
            Run(context =>
            {
                meetingRepo.DeleteAllMeetingsWithRoom(meetingRoom.Id);
                context.MeetingRooms.Remove(context.MeetingRooms.Find(meetingRoom.Id));
                context.SaveChanges();
            });
        }
    }
}