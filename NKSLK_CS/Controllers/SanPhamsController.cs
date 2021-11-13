﻿using System;
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
    public class SanPhamsController : Controller
    {
        private NKSLK_Context db = new NKSLK_Context();

        // GET: SanPhams
        public ActionResult Index()
        {
            return View(db.SanPham.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham model)
        {
            db.SanPham.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialEdit", sanPham);
        }

        [HttpPost]
        public ActionResult Edit(SanPham sanPham)
        {
            var result = db.SanPham.SingleOrDefault(b => b.id == sanPham.id);

            if (result != null)
            {
                result.ten = sanPham.ten;
                result.so_dang_ky = sanPham.so_dang_ky;
                result.ngay_dang_ky = sanPham.ngay_dang_ky;
                result.ngay_san_xuat = sanPham.ngay_san_xuat;
                result.han_su_dung = sanPham.han_su_dung;
                result.quy_cach = sanPham.quy_cach;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialDelete", sanPham);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            SanPham sanPham = db.SanPham.Find(id);
            db.SanPham.Remove(sanPham);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string searchString, int category)
        {
            List<SanPham> sanPham = new List<SanPham>();
            if (!String.IsNullOrEmpty(searchString))
            {
                if (category == 1)
                {
                    sanPham = db.SanPham.Where(x => x.ten.Contains(searchString)).ToList();
                }
                //else if (category == 2)
                //{
                //    sanPham = db.SanPham.SqlQuery("select * from SanPham where ((DATEDIFF(year, ngay_san_xuat, han_su_dung)) >=" + searchString).ToList();
                //}
                //else
                //{
                //    congNhan = db.CongNhan.SqlQuery("select * from CongNhan, PhongBan where PhongBan.ten like '%" + searchString + "%'").ToList();
                //    ViewBag.phongban = new CongNhansController().Chucvu();
                //    ViewBag.phuong = new CongNhansController().getPhuong();
                //}
            }
            else
            {
                sanPham = db.SanPham.Where(x => x.id != 0).ToList();
            }
            return View("Index", sanPham);
        }
    }
}