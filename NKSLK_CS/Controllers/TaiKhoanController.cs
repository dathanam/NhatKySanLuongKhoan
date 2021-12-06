using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NKSLK_CS.Controllers
{
    
    public class TaiKhoanController : Controller
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
            if(ModelState.IsValid) /*Nếu người dùng nhập đầy đủ các thông tin thì mới cho gọi phương thức addTaiKhoan*/
            {
                DSTaiKhoan dSTaiKhoan = new DSTaiKhoan();
                dSTaiKhoan.addTaiKhoan(taiKhoan);
                return RedirectToAction("Index");
            }
            
            
                return View();
        }
        

        /*Sửa Tài Khoản*/
        public ActionResult Edit (int ? id)
        {
            DSTaiKhoan dstaiKhoan = new DSTaiKhoan();
            if (id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
              TaiKhoan taiKhoan = dstaiKhoan.GetTaiKhoanByID(id.Value);
           // TaiKhoan taiKhoan = db.TaiKhoan.Find(id.Value);
            if(taiKhoan== null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialEdit", taiKhoan);
        }

        [HttpPost]

        public ActionResult Edit (TaiKhoan taiKhoan)
        {
            DSTaiKhoan dstaiKhoan = new DSTaiKhoan();
            dstaiKhoan.editTaiKhoan(taiKhoan);
            return RedirectToAction("Index");
        }

        /*Xóa tài khoản*/
        public ActionResult Delete(int ? id)
        {
            DSTaiKhoan dstaiKhoan = new DSTaiKhoan();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = dstaiKhoan.GetTaiKhoanByID(id.Value);
            // TaiKhoan taiKhoan = db.TaiKhoan.Find(id.Value);
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

    }
}