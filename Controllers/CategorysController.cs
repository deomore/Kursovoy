﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompShop2.Models;

namespace CompShop2.Controllers
{
    public class CategorysController : Controller
    {
        private CSEntities db = new CSEntities();
        public ActionResult Index()
        {
            return View(db.Categorys.ToList());
        }

        // GET: Categorys/Details/5
        public ActionResult Details(int id)
        {
            Categorys categorys = db.Categorys.Find(id);
            if (categorys == null)
            {
                return HttpNotFound();
            }
            return View(categorys);
        }

        // GET: Categorys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorys/Create
        [HttpPost]
        public ActionResult Create(Categorys categorys)
        {
            if (ModelState.IsValid)
            {
                db.Categorys.Add(categorys);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categorys);
        }

        // GET: Categorys/Edit/5
        public ActionResult Edit(int id)
        {
            Categorys categorys = db.Categorys.Find(id);
            if (categorys == null)
            {
                return HttpNotFound();
            }
            return View(categorys);
        }

        // POST: Categorys/Edit/5
        [HttpPost]
        public ActionResult Edit(Categorys categorys)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorys).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorys);
        }

        // GET: Categorys/Delete/5
        public ActionResult Delete(int id)
        {
            Categorys categorys = db.Categorys.Find(id);
            if (categorys == null)
            {
                return HttpNotFound();
            }
            return View(categorys);

        }

      
    }
}