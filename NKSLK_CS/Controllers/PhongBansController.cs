using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NKSLK_CS.Controllers
{
    public class PhongBansController : Controller
    {
        private NKSLK_Context db = new NKSLK_Context();
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
        public ActionResult Create(PhongBan phongBan)
        {
            if (ModelState.IsValid)
            {
                PhongBanList phongBanList = new PhongBanList();
                phongBanList.AddPhongBan(phongBan);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongBan phongBan = db.PhongBan.Find(id);
            if (phongBan == null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialEdit", phongBan);
        }


        // Edit
        /*
         public ActionResult Edit (string id = "")
        {
            PhongBanList phongBanList = new PhongBanList();
            List<PhongBan> obj = phongBanList.getPhongBan(id);
            return View(obj.FirstOrDefault());    
        }
        */
        [HttpPost]
        public ActionResult Edit(PhongBan phongBan)
        {
            PhongBanList phongBanList = new PhongBanList();
            phongBanList.EditPhongBan(phongBan);
            return RedirectToAction("Index");
        }

        // delete
        /*
        public ActionResult Delete(string id = "")
        {
            PhongBanList phongBanList = new PhongBanList();
            List<PhongBan> obj = phongBanList.getPhongBan(id);
            return View(obj.FirstOrDefault());
        }
        */

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongBan phongBan = db.PhongBan.Find(id);
            if (phongBan == null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialDelete", phongBan);
        }


        [HttpPost]

        public ActionResult Delete(PhongBan phongBan)
        {
            PhongBanList phongBanList = new PhongBanList();
            phongBanList.DeletePhongBan(phongBan);
            return RedirectToAction("Index");
        }

        [HttpPost]

        public ActionResult Index(string searching)
        {
            var links = from l in db.PhongBan select l;
            if (!String.IsNullOrEmpty(searching)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.ten.Contains(searching)); //lọc theo chuỗi tìm kiếm
            }

            return View(links); //trả về kết quả
        }



    }
}