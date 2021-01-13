using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CompShop2.Models;
using System.Data.Entity.Validation;

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
            try
            {
                DateTime date = DateTime.Now.AddDays(-days);
                IEnumerable<Transaction> transaction = db.Transaction.Where(w => w.Seller == ProdID && w.Date > date).Include(w =>w.Goods).ToList();

                return View(transaction);
            }
            catch
            {
                return RedirectToAction("Index","Home");
            }
        }
        [Authorize(Roles = "Manager")]
        public ActionResult Extra(int id, double sum)
        {
           Salary salary = db.Salary.Where(l => l.SellerID == id).First() ;
       salary.Extras += sum;
            db.SaveChanges();
       salary.Finaly = (double)(salary.Base + salary.Extras);
       db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        // GET: Workers/Edit/5
        public ActionResult Edit(int id)
        {
            Workers workers = db.Workers.Find(id);
            if (workers == null)
            {
                return HttpNotFound();
            }
            return View(workers);
        }

        // POST: Workers/Edit/5
        [HttpPost]
        public ActionResult Edit(Workers workers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(workers).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(workers);
            }
            catch (DbEntityValidationException e)
            {
                Session["error"] = e.EntityValidationErrors;
                return RedirectToAction("Index");
            }
        }

        // GET: Workers/Delete/5


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            db.Workers.Remove(db.Workers.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
