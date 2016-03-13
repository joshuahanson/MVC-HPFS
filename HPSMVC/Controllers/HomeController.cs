﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HPSMVC.DAL;
using HPSMVC.Models;

namespace HPSMVC.Controllers
{
    public class HomeController : Controller
    {
        private HPSMVCEntities db = new HPSMVCEntities();

        // GET: Home
        public ActionResult Admin()
        {
            var indices = db.Indices.Include(i => i.File);
            return View(indices.ToList());
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Index index = db.Indices.Find(id);
            if (index == null)
            {
                return HttpNotFound();
            }
            return View(index);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            ViewBag.FileID = new SelectList(db.Files, "ID", "fileName");
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,ButtonText,ButtonLink,FileID")] Index index)
        {
            if (ModelState.IsValid)
            {
                db.Indices.Add(index);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FileID = new SelectList(db.Files, "ID", "fileName", index.FileID);
            return View(index);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Index index = db.Indices.Find(id);
            if (index == null)
            {
                return HttpNotFound();
            }
            ViewBag.FileID = new SelectList(db.Files, "ID", "fileName", index.FileID);
            return View(index);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,ButtonText,ButtonLink,FileID")] Index index)
        {
            if (ModelState.IsValid)
            {
                db.Entry(index).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FileID = new SelectList(db.Files, "ID", "fileName", index.FileID);
            return View(index);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Index index = db.Indices.Find(id);
            if (index == null)
            {
                return HttpNotFound();
            }
            return View(index);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Index index = db.Indices.Find(id);
            db.Indices.Remove(index);
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
    }
}
