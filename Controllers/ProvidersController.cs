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
        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {
            return View(db.Providers.ToList());
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Details(int id = 0)
        {
            Providers providers= db.Providers.Find(id);
            if (providers == null)
            {
                return HttpNotFound();
            }
            return View(providers);
        }

       
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
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

       [Authorize(Roles = "Manager")]
            public ActionResult Edit(int id = 0)
        {
            Providers providers = db.Providers.Find(id);
            if (providers == null)
            {
                return HttpNotFound();
            }
            return View(providers);
        }

        [HttpPost]
        public ActionResult Edit(Providers providers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(providers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(providers);
        }


        [Authorize(Roles = "Manager")]
        public ActionResult Delete(int id = 0)
        {
            Providers providers = db.Providers.Find(id);
            if (providers == null)
            {
                return HttpNotFound();
            }
            return View(providers);
        }

            [HttpPost, ActionName("Delete")]
            public ActionResult DeleteConfirmed(int id)
            {
                db.Providers.Remove(db.Providers.Find(id));
                db.SaveChanges();
                return RedirectToAction("Index");
           }
        
    }
}
