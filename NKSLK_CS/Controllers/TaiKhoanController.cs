using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKSLK_CS.Controllers
{
    public class TaiKhoanController : Controller
    {
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
            else
            {
                return View();
            }
        }

        /*Sửa Tài Khoản*/
        public ActionResult Edit (string id= "")
        {
            DSTaiKhoan dstaiKhoan = new DSTaiKhoan();
            List<TaiKhoan> obj = dstaiKhoan.GetTaikhoans(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]

        public ActionResult Edit (TaiKhoan taiKhoan)
        {
            DSTaiKhoan dstaiKhoan = new DSTaiKhoan();
            dstaiKhoan.editTaiKhoan(taiKhoan);
            return RedirectToAction("Index");
        }

        /*Xóa tài khoản*/
        public ActionResult Delete(string id = "")
        {
            DSTaiKhoan dstaiKhoan = new DSTaiKhoan();
            List<TaiKhoan> obj = dstaiKhoan.GetTaikhoans(id);
            return View(obj.FirstOrDefault());
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