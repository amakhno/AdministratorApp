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
    public class MeetingRoomController : Controller
    {
        private IMeetingsRepository _mdb;
        readonly IMeetingRoomRepository _mrdb;

        public MeetingRoomController(IMeetingRoomRepository mrdb, IMeetingsRepository mdb)
        {
            _mdb = mdb;
            _mrdb = mrdb;
        }

        // GET: MeetingRooms
        public ActionResult Index()
        {
            return View(_mrdb.GetAllRooms());
        }

        // GET: MeetingRooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingRoom meetingRoom = _mrdb.GetMeetingRoomById((int)id);
            if (meetingRoom == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView(meetingRoom);
            }
            return View(meetingRoom);
        }

        // GET: MeetingRooms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeetingRooms/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(MeetingRoom meetingRoom)
        {
            if (ModelState.IsValid)
            {
                _mrdb.AddMeetingRoom(meetingRoom);
                return RedirectToAction("Index");
            }
            return View(meetingRoom);
        }

        // GET: MeetingRooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingRoom meetingRoom = _mrdb.GetMeetingRoomById((int)id);
            if (meetingRoom == null)
            {
                return HttpNotFound();
            }
            return View(meetingRoom);
        }

        // POST: MeetingRooms/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MeetingRoom meetingRoom)
        {
            if (ModelState.IsValid)
            {
                _mrdb.UpdateMeetingRoom(meetingRoom);
                return RedirectToAction("Index");
            }
            return View(meetingRoom);
        }

        // GET: MeetingRooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeetingRoom meetingRoom = _mrdb.GetMeetingRoomById((int)id);
            if (meetingRoom == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView(meetingRoom);
            }
            return View(meetingRoom);
        }

        // POST: MeetingRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MeetingRoom meetingRoom = _mrdb.GetMeetingRoomById(id);
            _mrdb.RemoveMeetingRoom(meetingRoom, _mdb);
            return RedirectToAction("Index");
        }
    }
}