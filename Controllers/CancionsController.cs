using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HM_Proyecto_DF.Models;

namespace HM_Proyecto_DF.Controllers
{
    public class CancionsController : Controller
    {
        private CancionesDFEntities db = new CancionesDFEntities();

        // GET: Cancions
        public ActionResult Index()
        {
            var cancion = db.Cancion.Include(c => c.Album);
            return View(cancion.ToList());
        }

        // GET: Cancions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancion cancion = db.Cancion.Find(id);
            if (cancion == null)
            {
                return HttpNotFound();
            }
            return View(cancion);
        }

        // GET: Cancions/Create
        public ActionResult Create()
        {
            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "NombreAlbum");
            return View();
        }

        // POST: Cancions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CancionID,NombreCancion,Duracion,AnioLanzamiento,Compositor,AlbumID")] Cancion cancion)
        {
            if (ModelState.IsValid)
            {
                db.Cancion.Add(cancion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "NombreAlbum", cancion.AlbumID);
            return View(cancion);
        }

        // GET: Cancions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancion cancion = db.Cancion.Find(id);
            if (cancion == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "NombreAlbum", cancion.AlbumID);
            return View(cancion);
        }

        // POST: Cancions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CancionID,NombreCancion,Duracion,AnioLanzamiento,Compositor,AlbumID")] Cancion cancion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cancion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlbumID = new SelectList(db.Album, "AlbumID", "NombreAlbum", cancion.AlbumID);
            return View(cancion);
        }

        // GET: Cancions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cancion cancion = db.Cancion.Find(id);
            if (cancion == null)
            {
                return HttpNotFound();
            }
            return View(cancion);
        }

        // POST: Cancions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cancion cancion = db.Cancion.Find(id);
            db.Cancion.Remove(cancion);
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
