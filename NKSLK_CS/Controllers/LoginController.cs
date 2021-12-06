using NKSLK_CS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace NKSLK_CS.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index (TaiKhoan taiKhoan)
        {
            DSTaiKhoan dSTaiKhoan = new DSTaiKhoan();
            var result = dSTaiKhoan.login(taiKhoan.tendangnhap, taiKhoan.matkhau);
            if(result && ModelState.IsValid)
            {
                var listNhom = dSTaiKhoan.GetListIDNhom(taiKhoan.tendangnhap);
                SessionHelper.SetSession(new UserSession() { tendangnhap = taiKhoan.tendangnhap });
                Session.Add("SESSION_GROUP", listNhom);
                return RedirectToAction("Index", "PhongBans");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập không đúng");
            }
            return View(taiKhoan);
        }
    }
    [Serializable]
    public class UserSession
    {
        public string tendangnhap { set; get; }
        public string matkhau { set; get; }
    }

    public class SessionHelper
    {
        public static void SetSession(UserSession session)
        {
            HttpContext.Current.Session["loginSession"] = session;
        }
        public static UserSession GetSession()
        {
            var session = HttpContext.Current.Session["loginSession"];
            if(session == null)
                return null;
            else
            {
                return session as UserSession;
            }
        }
    }

    


}