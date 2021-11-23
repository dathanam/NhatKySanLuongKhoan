using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NKSLK_CS.Controllers
{
    public class TaiKhoanwController : Controller
    {
        private NKSLK_Context db = new NKSLK_Context();
        // GET: TaiKhoan
        public ActionResult Index()
        {
            DSTaiKhoan dSTaiKhoan = new DSTaiKhoan();
            List<TaiKhoan> obj = dSTaiKhoan.GetTaikhoans(String.Empty);
            return View(obj);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid) /*Nếu người dùng nhập đầy đủ các thông tin thì mới cho gọi phương thức addTaiKhoan*/
            {
                DSTaiKhoan dSTaiKhoan = new DSTaiKhoan();
                dSTaiKhoan.addTaiKhoan(taiKhoan);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        /*Sửa Tài Khoản*/

        /* 
          public ActionResult Edit(int? id)
          {
              if (id == null)
              {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
              }

              TaiKhoan taiKhoan = db.TaiKhoan.Find(id);
              if (taiKhoan == null)
              {
                  return HttpNotFound();
              }
              return PartialView("PartialEdit", taiKhoan);
          }
        */

        
        public ActionResult Edit(string id = "")
        {
            DSTaiKhoan dstaiKhoan = new DSTaiKhoan();
            List<TaiKhoan> obj = dstaiKhoan.GetTaikhoans(id);
            return View(obj.FirstOrDefault());
        }
        

        [HttpPost]
        public ActionResult Edit(TaiKhoan taiKhoan)
        {
            DSTaiKhoan dstaiKhoan = new DSTaiKhoan();
            dstaiKhoan.editTaiKhoan(taiKhoan);
            return RedirectToAction("Index");
        }

        /*Xóa tài khoản*/
        /*
        public ActionResult Delete(string id = "")
        {
            DSTaiKhoan dstaiKhoan = new DSTaiKhoan();
            List<TaiKhoan> obj = dstaiKhoan.GetTaikhoans(id);
            return View(obj.FirstOrDefault());
        }
        */

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoan.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialDelete", taiKhoan);
        }


        [HttpPost]

        public ActionResult Delete(TaiKhoan taiKhoan)
        {
            DSTaiKhoan dstaiKhoan = new DSTaiKhoan();
            dstaiKhoan.deleteTaiKhoan(taiKhoan);
            return RedirectToAction("Index");
        }


        public ActionResult DangNhap(String tendangnhap, String matkhau)
        {
            DSTaiKhoan dstaiKhoan = new DSTaiKhoan();
            var x = dstaiKhoan.dangnhap(tendangnhap, matkhau);
            if (x == "1")
            {
                return RedirectToAction("Index", "TaiKhoan");
            }
            if (x == "0")
            {
                ModelState.AddModelError("", "Đăng Nhập không Đúng");
            }
            return RedirectToAction("Index", "Login");
        }
    }
}