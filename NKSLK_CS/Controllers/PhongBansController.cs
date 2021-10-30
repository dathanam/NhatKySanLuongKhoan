using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKSLK_CS.Controllers
{
    public class PhongBansController : Controller
    {
        // GET: PhongBans
        public ActionResult Index()
        {
            PhongBanList strPB = new PhongBanList();
            List<PhongBan> obj = strPB.getPhongBan(string.Empty);
            return View(obj);
        }
    }
}