using AdministratorNegotiating.Models;
using AdministratorNegotiating.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace AdministratorNegotiating.Controllers
{
    public class MeetingsGeneralApiController : ApiController
    {
        private IUserManagerRepository _udb;
        private IMeetingsRepository _mdb;

        public MeetingsGeneralApiController(IUserManagerRepository udb, IMeetingsRepository mbd)
        {
            _udb = udb;
            _mdb = mbd;
        }

        // GET: api/MeetingRoomApi
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public string[] Get(string username, string password)
        {
            if (_udb.Login(username, password))
            {
                return _mdb.GetUserListInfo();
            }
            else
            {
                return new string[] { "error" };
            }
        }

        [Authorize]
        public JsonResult<MeetingTableUserPosition[]> Get()
        {            
            return Json(_mdb.GetUserListInfo(true));
            /*Meeting meetingToSave = new Meeting();
            try
            {
                meetingToSave.BeginTime = meeting.BeginTime;
                meetingToSave.EndTime = meeting.EndTime;
                meetingToSave.NameOfMeeting = meeting.NameOfMeeting;
                meetingToSave.DayOfBooking = DateTime.Now;
                meetingToSave.UserName = meeting.UserName;
                meetingToSave.Status = Meeting.StatusTypes.Waiting;
                meetingToSave.MeetingRoomId = meeting.MeetingRoomId;
                if (_mdb.isAllow(meeting.MeetingRoomId, meeting.BeginTime, meeting.EndTime))
                {
                    _mdb.Add(meetingToSave);
                    return "ok";
                }
                else
                {
                    throw new Exception("Команата занята или даты некорректны");
                }
            }
            catch (Exception exc)
            {
                return exc.Message;
            }*/
        }

        // POST: api/MeetingRoomApi
        public string Post(MeetingAddViewModel meeting)
        {
            if (!_udb.Login(meeting.UserName, meeting.Password))
            {
                return "Ошибка входа";
            }
            Meeting meetingToSave = new Meeting();
            try
            {
                meetingToSave.BeginTime = meeting.BeginTime;
                meetingToSave.EndTime = meeting.EndTime;
                meetingToSave.NameOfMeeting = meeting.NameOfMeeting;
                meetingToSave.DayOfBooking = DateTime.Now;
                meetingToSave.UserName = meeting.UserName;
                meetingToSave.Status = Meeting.StatusTypes.Waiting;
                meetingToSave.MeetingRoomId = meeting.MeetingRoomId;
                if (_mdb.isAllow(meeting.MeetingRoomId, meeting.BeginTime, meeting.EndTime))
                {
                    _mdb.Add(meetingToSave);
                    return "ok";
                }
                else
                {
                    throw new Exception("Команата занята или даты некорректны");
                }
            }
            catch(Exception exc)
            {
                return exc.Message;
            }
        }
    }
}
