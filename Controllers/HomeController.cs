using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CompShop2.Models;

namespace CompShop2.Controllers
{
    public class HomeController : Controller
    {
     private  CSEntities db = new CSEntities();
        public ActionResult Index()
        {
            return View(db.Goods.ToList());
        }

       
        public ActionResult Details(int id = 0)
        {
            Goods goods = db.Goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        // [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Goods goods)
        {
            if (ModelState.IsValid)
            {
                db.Goods.Add(goods);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(goods);
        }

        // [Authorize(Roles = "Seller")]
        public ActionResult Edit(int id = 0)
        {
           Goods goods = db.Goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        [HttpPost]
        public ActionResult Edit(Goods goods)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goods).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(goods);
        }
       
        public ActionResult Spisat(int ProdID)
        {
           DateTime date = DateTime.Now.AddMonths(-1);
            bool res = db.Transaction.Any(w => w.Thing == ProdID && w.Date > date);
            if (res)
            {
                Goods goods = db.Goods.Where(z => z.GoodsID == ProdID).First();
                goods.Quantity = 0;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        

       public string GetInfo()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;
           // var a = HttpContext.User.Identity.
            return "<p> Адресс  " +  email;
        }

        [Authorize(Roles = "Seller")]
        public ActionResult Sell(int ProdID, int count)
        {
            // этот блок получает мыло активного пользователя
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;
            //var rolle = HttpContext.User.IsInRole("seller");
            string c = email;

            // проверка в таблице 
            Workers workers = db.Workers.Where(l => l.Name == c).First();
            int z = workers.WorkerID;

            // 
            Goods goods = db.Goods.Where(k => k.GoodsID == ProdID).First();
            goods.Quantity -= count;
            db.SaveChanges();

            Transaction transaction = new Transaction()
            {
                Seller = z,
                Thing = ProdID,
                Date = DateTime.Now
            };
            db.Transaction.Add(transaction);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // [Authorize(Roles = "Seller")]
        public ActionResult Delete(int id = 0)
        {
            Goods goods = db.Goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

    }
}