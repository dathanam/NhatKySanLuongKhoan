using NKSLK_CS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKSLK_CS.Controllers
{
    public class TaiKhoansController : Controller
    {
        // GET: TaiKhoans
        public ActionResult Index()
        {
            DSTaiKhoan dSTaiKhoan = new DSTaiKhoan();
            List<taikhoan> obj = dSTaiKhoan.GetTaikhoans(String.Empty);
            return View(obj);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create (taikhoan taikhoan)
        {
            if (ModelState.IsValid)
            {
                DSTaiKhoan dSTaiKhoan = new DSTaiKhoan();
                dSTaiKhoan.addTaiKhoan(taikhoan);
                return RedirectToAction("Index");
            }
            return View();

        }



    }
}