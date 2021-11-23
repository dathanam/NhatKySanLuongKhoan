using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NKSLK_CS.Controllers
{
    public class ThongKesController : Controller
    {
        // GET: ThongKes
        public ActionResult Index()
        {
            DSThongKe dSThongKe = new DSThongKe();
            List<ThongKe> obj = dSThongKe.DSThongKes(String.Empty);
            return View(obj);
        }
    }
}