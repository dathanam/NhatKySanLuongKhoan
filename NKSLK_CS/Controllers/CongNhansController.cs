using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NKSLK_CS;

namespace NKSLK_CS.Controllers
{
    public class CongNhansController : Controller
    {
        private NKSLK_Context db = new NKSLK_Context();

        // GET: CongNhans
        public ActionResult Index()
        {
            var CongNhan = db.CongNhan.Where(x => x.id != 0);
            return View(CongNhan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CongNhan model)
        {
            db.CongNhan.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CongNhans/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CongNhan congNhan = db.CongNhan.Find(id);
        //    if (congNhan == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.id_phong_ban = new SelectList(db.PhongBan, "id", "ten", congNhan.id_phong_ban);
        //    ViewBag.id_phuong = new SelectList(db.Phuong, "id", "ten", congNhan.id_phuong);
        //    return View(congNhan);
        //}

        //// POST: CongNhans/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,ten,ngay_sinh,gioi_tinh,chuc_vu,que_quan,luong_hop_dong,luong_bao_hiem,id_phong_ban,id_phuong")] CongNhan congNhan)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(congNhan).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.id_phong_ban = new SelectList(db.PhongBan, "id", "ten", congNhan.id_phong_ban);
        //    ViewBag.id_phuong = new SelectList(db.Phuong, "id", "ten", congNhan.id_phuong);
        //    return View(congNhan);
        //}

        //// GET: CongNhans/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CongNhan congNhan = db.CongNhan.Find(id);
        //    if (congNhan == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(congNhan);
        //}

        //// POST: CongNhans/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CongNhan congNhan = db.CongNhan.Find(id);
        //    db.CongNhan.Remove(congNhan);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}