using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompShop2.Models;

namespace CompShop2.Controllers
{
    public class ProvidersController : Controller
    {
        private CSEntities db = new CSEntities();
        public ActionResult Index()
        {
            return View(db.Providers.ToList());
        }


        public ActionResult Details(int id = 0)
        {
            Providers providers= db.Providers.Find(id);
            if (providers == null)
            {
                return HttpNotFound();
            }
            return View(providers);
        }

        // [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Providers providers)
        {
            if (ModelState.IsValid)
            {
                db.Providers.Add(providers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(providers);
        }

        // [Authorize(Roles = "Seller")]

        public ActionResult Edit(int id = 0)
        {
            Providers providers = db.Providers.Find(id);
            if (providers == null)
            {
                return HttpNotFound();
            }
            return View(providers);
        }

      

        // [Authorize(Roles = "Seller")]
        public ActionResult Delete(int id = 0)
        {
            Providers providers = db.Providers.Find(id);
            if (providers == null)
            {
                return HttpNotFound();
            }
            return View(providers);
        }
    }
}
