using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministratorNegotiating.Models.Repositories.Interfaces
{
    public interface IMeetingRoomRepository
    {
        List<MeetingRoom> GetAllRooms();
        MeetingRoom GetMeetingRoomById(int id);
        void AddMeetingRoom(MeetingRoom meetingRoom);
        void UpdateMeetingRoom(MeetingRoom meetingRoom);
        void RemoveMeetingRoom(MeetingRoom meetingRoom, IMeetingsRepository repo);
    }
}
