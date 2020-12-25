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
    public class WorkersController : Controller
    {
        private CSEntities db = new CSEntities();


        
        // GET: Workers
        public ActionResult Index()
        {
            var Cat = db.Workers.Include(p => p.Salary);
            return View(Cat.ToList());
        }
        
        // GET: Workers/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Workers workers)
        {
            if (ModelState.IsValid)
            {
                db.Workers.Add(workers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workers);
        }

       
        public ActionResult Account()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;
            string c = email;

            Workers workers = db.Workers.Where(l => l.Name == c).First();
           
            return View(workers);
        }



        [Authorize(Roles = "Manager")]
        public ActionResult Prodaga(int ProdID,int days)
        {
            DateTime date = DateTime.Now.AddDays(-days);
               IEnumerable <Transaction> transaction = db.Transaction.Where(w => w.Seller == ProdID && w.Date > date).ToList();

            return View(transaction);
        }
        [Authorize(Roles = "Manager")]
        public ActionResult Extra(int id, int sum)
        {
           Salary salary = db.Salary.Where(l => l.SellerID == id).First() ;
       salary.Extras = sum;
            db.SaveChanges();
       salary.Finaly = salary.Base + sum;
       db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        // GET: Workers/Edit/5
        public ActionResult Edit(int id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = HttpContext.User.Identity.Name;
            Workers workers = db.Workers.Where(l =>l.Name == email).First() ;
            return View(workers);
        }

        // POST: Workers/Edit/5
        [HttpPost]
        public ActionResult Edit(Workers workers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Account");
            }
            return View(workers);
        }

        // GET: Workers/Delete/5


        [Authorize(Roles = "Admin")]
        // POST: Workers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            Workers workers = db.Workers.Find(id);
            if (workers == null)
            {
                return HttpNotFound();
            }
            return View(workers);
        }
    }
}
