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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MeetingRoomApiController : ApiController
    {
        private IUserManagerRepository _udb;
        private IMeetingsRepository _mdb;

        public MeetingRoomApiController(IUserManagerRepository udb, IMeetingsRepository mbd)
        {
            _udb = udb;
            _mdb = mbd;
        }

        ApplicationDbContext context = new ApplicationDbContext();
        // GET: api/MeetingRoomApi
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public string Get(string username, string password)
        {
            if (_udb.Login(username, password))
            {
                return _mdb.GetUserListInfo();
            }
            else
            {
                return "error";
            }
        }

        // GET: api/MeetingRoomApi/5
        public List<MeetingRoom> Get(int id)
        {
            return context.MeetingRooms.ToList();
        }

        // POST: api/MeetingRoomApi
        public string Post(Meeting meeting)
        {            
            meeting.Status = Meeting.StatusTypes.Waiting;
            meeting.DayOfBooking = DateTime.Now;
            if (_mdb.isAllow(meeting.MeetingRoomId, meeting.BeginTime, meeting.EndTime))
            {
                _mdb.Add(meeting);
                return "ok";
            }
            else
            {
                return "Команата занята или даты некорректны";
            }
        }

        // PUT: api/MeetingRoomApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MeetingRoomApi/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }
    }
}
