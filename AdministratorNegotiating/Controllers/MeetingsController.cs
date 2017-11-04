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

namespace AdministratorNegotiating.Controllers
{
    [Authorize]
    public class MeetingsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        readonly IMeetingsRepository _mdb;

        public MeetingsController(IMeetingsRepository db)
        {
            _mdb = db;
        }

        // GET: Meetings
        public ActionResult Index()
        {
            var meetings = db.Meetings.Include(m => m.MeetingRoom);
            return View(meetings.ToList());
        }

        // GET: Meetings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // GET: Meetings/Create
        public ActionResult Create()
        {
            ViewBag.MeetingRoomId = new SelectList(db.MeetingRooms, "Id", "Name");
            return View();
        }

        // POST: Meetings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DayOfBooking,NameOfMeeting,BeginTime,EndTime,MeetingRoomId,UserName,Status")] Meeting meeting)
        {
            meeting.DayOfBooking = DateTime.Now;
            meeting.UserName = User.Identity.Name;

            bool isAllowed = true;
            ViewBag.Error = "";
            var meetingRoom = db.MeetingRooms.Find(meeting.MeetingRoomId);

            if (meetingRoom == null)
            {
                isAllowed = false;
                ViewBag.Error = "Комната с таким Id не найдена";
            }

            if (!_mdb.isAllow(meeting.MeetingRoomId, meeting.BeginTime, meeting.EndTime))
            {
                isAllowed = false;
                ViewBag.Error = "Комната занята в это время";
            }
            
            if (ModelState.IsValid && isAllowed)
            {
                db.Meetings.Add(meeting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MeetingRoomId = new SelectList(db.MeetingRooms, "Id", "Name", meeting.MeetingRoomId);
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = _mdb.GetById((int)id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        public ActionResult ListOfWaitingMeetingsPartial()
        {
            List<Meeting> meetings = _mdb.ListOfWaitingMeetings();
            return PartialView(meetings);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = db.Meetings.Find(id);
            db.Meetings.Remove(meeting);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Confirm(int id)
        {
            _mdb.Confirm(id);
            return RedirectToAction("Index");
        }
    }
}
