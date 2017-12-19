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
    [Authorize(Roles = "Administrators")]
    public class MeetingsController : Controller
    {
        readonly IMeetingsRepository _mdb;

        public MeetingsController(IMeetingsRepository mdb)
        {
            _mdb = mdb;
        }

        // GET: Meetings
        public ActionResult Index()
        {
            return View();
        }

        // GET: Meetings/Details/5
        public ActionResult Details(int? id)
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

        // GET: Meetings/Create
        public ActionResult Create()
        {
            ViewBag.MeetingRoomId = new SelectList(_mdb.GetAllRooms(), "Id", "Name");
            return View();
        }

        // POST: Meetings/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Meeting meeting)
        {
            meeting.DayOfBooking = DateTime.Now;
            meeting.UserName = User.Identity.Name;

            bool isAllowed = true;
            ViewBag.Error = "";

            if (!_mdb.isAllow(meeting.MeetingRoomId, meeting.BeginTime, meeting.EndTime))
            {
                isAllowed = false;
                ViewBag.Error = "Комната занята в это время";
            }

            if (ModelState.IsValid && isAllowed)
            {
                _mdb.Add(meeting);
                return RedirectToAction("Index");
            }

            ViewBag.MeetingRoomId = new SelectList(_mdb.GetAllRooms(), "Id", "Name", meeting.MeetingRoomId);

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
            return PartialView(meeting);
        }

        public ActionResult ListOfWaitingMeetingsPartial()
        {
            List<Meeting> meetings = _mdb.ListOfWaitingMeetings();
            return PartialView(meetings);
        }

        public ActionResult ListOfArchivePartial()
        {
            List<Meeting> meetings = _mdb.ListOfAcrhive();
            return PartialView(meetings);
        }

        public ActionResult ListOfInProcessPartial()
        {
            List<Meeting> meetings = _mdb.ListOfInProcess();
            return PartialView(meetings);
        }

        // POST: Meetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _mdb.DeleteById(id);
            return PartialView("TablesPartial");
        }

        public ActionResult Confirm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _mdb.Confirm((int)id);
            return PartialView("TablesPartial");//RedirectToAction("Index");
        }

        public ActionResult Reject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _mdb.Reject((int)id);
            return PartialView("TablesPartial"); // RedirectToAction("Index");
        }
    }
}
