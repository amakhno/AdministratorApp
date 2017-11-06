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
        List<Meeting> ListOfAcrhive();
        List<Meeting> ListOfInProcess();
        void Confirm(int id);
        void Reject(int id);
        void DeleteById(int id);
        void Add(Meeting meeting);
        string GetTimeInfo(int id);
        string GetUserListInfo();
        string GetByUserName(string username);
        List<MeetingRoom> GetAllRooms();
        MeetingRoom GetMeetingRoomById(int id);
        void AddMeetingRoom(MeetingRoom meetingRoom);
        void UpdateMeetingRoom(MeetingRoom meetingRoom);
        void RemoveMeetingRoom(MeetingRoom meetingRoom);
    }
}
