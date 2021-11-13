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
    public class DanhMucCongViecsController : Controller
    {
        private NKSLK_Context db = new NKSLK_Context();

        // GET: DanhMucCongViecs
        public ActionResult Index()
        {
            return View(db.DanhMucCongViec.ToList());
        }

        // GET: DanhMucCongViecs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DanhMucCongViec model)
        {
            db.DanhMucCongViec.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: DanhMucCongViecs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucCongViec danhMucCongViec = db.DanhMucCongViec.Find(id);
            if (danhMucCongViec == null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialEdit", danhMucCongViec);
        }

        // POST: DanhMucCongViecs/Edit/5
        [HttpPost]
        public ActionResult Edit(DanhMucCongViec danhMucCongViec)
        {
            var result = db.DanhMucCongViec.SingleOrDefault(b => b.id == danhMucCongViec.id);

            if (result != null)
            {
                result.ten = danhMucCongViec.ten;
                result.dinh_muc_khoan = danhMucCongViec.dinh_muc_khoan;
                result.don_vi_khoan = danhMucCongViec.don_vi_khoan;
                result.he_so_khoan = danhMucCongViec.he_so_khoan;
                result.dinh_muc_lao_dong = danhMucCongViec.dinh_muc_lao_dong;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        //Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMucCongViec danhMucCongViec = db.DanhMucCongViec.Find(id);
            if (danhMucCongViec == null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialDelete", danhMucCongViec);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            DanhMucCongViec danhMucCongViec = db.DanhMucCongViec.Find(id);
            db.DanhMucCongViec.Remove(danhMucCongViec);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string searchString, int category)
        {
            List<DanhMucCongViec> danhMucCongViec = new List<DanhMucCongViec>();
            if (!String.IsNullOrEmpty(searchString))
            {
                if (category == 1)
                {
                    danhMucCongViec = db.DanhMucCongViec.Where(x => x.ten.Contains(searchString)).ToList();
                }
            }
            else
            {
                danhMucCongViec = db.DanhMucCongViec.Where(x => x.id != 0).ToList();
            }
            return View("Index", danhMucCongViec);
        }
    }
}
