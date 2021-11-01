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
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create (PhongBan phongBan)
        {
            if (ModelState.IsValid)
            {
                PhongBanList phongBanList = new PhongBanList();
                phongBanList.AddPhongBan(phongBan);
                return RedirectToAction("Index");
            }
            return View();
        }
        // Edit

        public ActionResult Edit (string id = "")
        {
            PhongBanList phongBanList = new PhongBanList();
            List<PhongBan> obj = phongBanList.getPhongBan(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhongBan phongBan)
        {
            PhongBanList phongBanList = new PhongBanList();
            phongBanList.EditPhongBan(phongBan);
            return RedirectToAction("Index");
        }

        // delete
        public ActionResult Delete(string id = "")
        {
            PhongBanList phongBanList = new PhongBanList();
            List<PhongBan> obj = phongBanList.getPhongBan(id);
            return View(obj.FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PhongBan phongBan)
        {
            PhongBanList phongBanList = new PhongBanList();
            phongBanList.DeletePhongBan(phongBan);
            return RedirectToAction("Index");
        }





    }
}