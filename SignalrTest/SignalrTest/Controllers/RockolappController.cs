using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalrTest.Controllers
{
    public class RockolappController : Controller
    {
        // GET: Rockolapp
        public ActionResult Index(string spotifyUserId)
        {
            ViewData["spotifyUserId"] = spotifyUserId;
            return View();
        }

        // GET: Rockolapp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Rockolapp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rockolapp/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rockolapp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Rockolapp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rockolapp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Rockolapp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
