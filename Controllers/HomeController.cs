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
            var Cat = db.Goods.Include(p => p.Categorys);
            return View(Cat.ToList());
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

        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            SelectList cat = new SelectList(db.Categorys, "CategotyID", "CatName");
            ViewBag.Categorys = cat;
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

        [Authorize(Roles = "Seller,Manager")]
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

        [Authorize(Roles = "Manager")]
        public ActionResult Spisat(int ProdID)
        {
           DateTime date = DateTime.Now.AddMonths(-1);
            try
            {
                int maxID = db.Transaction.Max(w => w.TransCode);
                Transaction transaction = db.Transaction.Where(w => w.Thing == ProdID && w.TransCode == maxID).First();
                //bool res = db.Transaction.Any(w => w.Thing == ProdID && w.Date < date);
                if (transaction.Date < date)
                {
                    Goods goods = db.Goods.Where(z => z.GoodsID == ProdID).First();
                    goods.Quantity = 0;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch { return RedirectToAction("Index"); }
            }

        [Authorize(Roles = "Seller,Manager")]
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

            // Продажа (уменьшение товара)
            Goods goods = db.Goods.Where(k => k.GoodsID == ProdID).First();
            if (count <= goods.Quantity)
            {
                goods.Quantity -= count;
                db.SaveChanges();

                // создание транзакции
                Transaction transaction = new Transaction()
                {
                    Seller = z,
                    Thing = ProdID,
                    Date = DateTime.Now
                };
                db.Transaction.Add(transaction);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Search(string GName)
        {
            var goods = db.Goods.Where(good => good.Name.Contains(GName));
            return PartialView(goods);
        }

        [Authorize(Roles = "Manager")]
       
        public ActionResult Delete(int id = 0)
        {
            Goods goods = db.Goods.Find(id);
            if (goods == null)
            {
                return HttpNotFound();
            }
            return View(goods);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Goods.Remove(db.Goods.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}