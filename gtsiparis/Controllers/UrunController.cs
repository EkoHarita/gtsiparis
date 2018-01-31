﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using gtsiparis;
using PagedList;
using PagedList.Mvc;
using gtsiparis.Models;

namespace gtsiparis.Controllers
{
    public class UrunController : Controller
    {
        private Model1 db = new Model1();

        // GET: Urun
        public ActionResult Index(int page)
        {
            var pager = new Pager(1, page);
            var urun = (from m in db.Urun
                        where m.Aktif == true
                        select m).Include(u => u.Birim).Include(u => u.Grup).Include(u => u.Kategori).Include(u => u.Sorumlu).Include(u => u.Uretici);

            pager = new Pager(urun.Count(), page);
            UrunListe viewModel = new UrunListe
            {
                UrunListesi = urun.OrderBy(x => x.Id).ToPagedList(page, 5).ToList(),
                Pager = pager
            };
            Session["Lastpager"] = page;
            return View(viewModel);
        }



        // GET: Urun/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // GET: Urun/Create
        public ActionResult Create()
        {
            ViewBag.Birim_Id = new SelectList(db.Birim, "Id", "BirimAdi");
            ViewBag.Grup_Id = new SelectList(db.Grup, "Id", "GrupAdi");
            ViewBag.Kategori_Id = new SelectList(db.Kategori, "Id", "KategoriAdi");
            ViewBag.Sorumlu_Id = new SelectList(db.Users, "Id", "AdSoyad");
            ViewBag.Uretici_Id = new SelectList(db.Users, "Id", "AdSoyad");
            ViewBag.StokId = new SelectList(db.Stok, "Id", "Id");

            return View();
        }

        // POST: Urun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,Aciklama,Maliyet,Fiyat,Mesafe,AlinacakLokasyon,UretimBolge,Baslangic,Bitis,Aktif,Birim_Id,StokId,Grup_Id,Kategori_Id,Sorumlu_Id,Uretici_Id,UrunGorseli,RowVersion")] Urun urun)
        {
            if (ModelState.IsValid)
            {
                db.Urun.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Index", new { page = Session["Lastpager"] });
            }
           
            ViewBag.Birim_Id = new SelectList(db.Birim, "Id", "BirimAdi", urun.Birim_Id);
            ViewBag.Grup_Id = new SelectList(db.Grup, "Id", "GrupAdi", urun.Grup_Id);
            ViewBag.Kategori_Id = new SelectList(db.Kategori, "Id", "KategoriAdi", urun.Kategori_Id);
            ViewBag.Sorumlu_Id = new SelectList(db.Users, "Id", "AdSoyad", urun.Kategori_Id);
            ViewBag.Uretici_Id = new SelectList(db.Users, "Id", "AdSoyad", urun.Uretici_Id);
            ViewBag.StokId = new SelectList(db.Stok, "Id", "Id", urun.StokId);
            return View(urun);
        }

        // GET: Urun/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }

           
            ViewBag.Birim_Id = new SelectList(db.Birim, "Id", "BirimAdi", urun.Birim_Id);
            ViewBag.Grup_Id = new SelectList(db.Grup, "Id", "GrupAdi", urun.Grup_Id);
            ViewBag.Kategori_Id = new SelectList(db.Kategori, "Id", "KategoriAdi", urun.Kategori_Id);
            ViewBag.Sorumlu_Id = new SelectList(db.Users, "Id", "AdSoyad", urun.Kategori_Id);
            ViewBag.Uretici_Id = new SelectList(db.Users, "Id", "AdSoyad", urun.Uretici_Id);
            ViewBag.StokId = new SelectList(db.Stok, "Id", "Id", urun.StokId);
            return View(urun);
        }

        // POST: Urun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,Aciklama,Maliyet,Fiyat,Mesafe,AlinacakLokasyon,UretimBolge,Baslangic,Bitis,Aktif,Birim_Id,StokId,Grup_Id,Kategori_Id,Sorumlu_Id,Uretici_Id,UrunGorseli,RowVersion")] Urun urun)
        {

            if (ModelState.IsValid)
            {
                db.Entry(urun).State = EntityState.Modified;
                db.SaveChanges();
                //int a = (from o in db.Urun where o.Id == urun.Id select o).
                return RedirectToAction("Index", new { page = Session["Lastpager"] });
            }
            ViewBag.Birim_Id = new SelectList(db.Birim, "Id", "BirimAdi", urun.Birim_Id);
            ViewBag.Grup_Id = new SelectList(db.Grup, "Id", "GrupAdi", urun.Grup_Id);
            ViewBag.Kategori_Id = new SelectList(db.Kategori, "Id", "KategoriAdi", urun.Kategori_Id);
            ViewBag.Sorumlu_Id = new SelectList(db.Users, "Id", "AdSoyad", urun.Kategori_Id);
            ViewBag.Uretici_Id = new SelectList(db.Users, "Id", "AdSoyad", urun.Uretici_Id);
            ViewBag.StokId = new SelectList(db.Stok, "Id", "Id", urun.StokId);
            return View(urun);
        }

        // GET: Urun/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Urun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urun urun = db.Urun.Find(id);
            db.Urun.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index", new { page = Session["Lastpager"] });
        }
        public ActionResult StoklariGoster(int id)
        {
            IEnumerable<Stok> StokListe;
            StokListe = (from b in db.Stok where b.UrunId == id select b).ToList();
            return PartialView("_StokGecmisi", StokListe);
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
