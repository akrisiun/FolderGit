using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ConsoleController : Controller
    {
        // GET: Console
        public ActionResult Index()
        {
            return View();
        }

        // GET: Console/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Console/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Console/Create
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

        // GET: Console/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Console/Edit/5
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

        // GET: Console/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Console/Delete/5
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
