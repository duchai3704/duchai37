using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_63131920.Context;

namespace Project_63131920.Areas.Admin.Controllers
{
    public class Products_63131920Controller : Controller
    {
        private QLCH_63131920Entities db = new QLCH_63131920Entities();

        // GET: Admin/Products_63131920

        public ActionResult Index(string name_search = "")
        {
            string cmd = "SELECT * FROM Products";
            if(name_search != "")
            {
                cmd += " WHERE product_name LIKE N'%" +name_search+ "%'";
            }
            var products = db.Products.SqlQuery(cmd);
            return View(products);
        }

        // GET: Admin/Products_63131920/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products_63131920/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Categories, "id", "name");
            return View();
        }

        // POST: Admin/Products_63131920/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,product_name,category_id,price,description,qty_in_stock,product_image")] Product product)
        {
            var imgPro = Request.Files["thumnail"];
            string postedFileName = System.IO.Path.GetFileName(imgPro.FileName);
            var path = Server.MapPath("/Assets/images/items" + postedFileName);
            imgPro.SaveAs(path);
            if (ModelState.IsValid)
            {
                product.product_image = postedFileName;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Categories, "id", "name", product.category_id);
            return View(product);
        }

        // GET: Admin/Products_63131920/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Categories, "id", "name", product.category_id);
            return View(product);
        }

        // POST: Admin/Products_63131920/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,product_name,category_id,price,description,qty_in_stock,product_image")] Product product)
        {
            try
            {
                var imgPro = Request.Files["thumnail"];
                string postedFileName = System.IO.Path.GetFileName(imgPro.FileName);
                var path = Server.MapPath("/Assets/images/items" + postedFileName);
                imgPro.SaveAs(path);
                product.product_image = postedFileName;
            }
            catch
            { }
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.Categories, "id", "name", product.category_id);
            return View(product);
        }

        // GET: Admin/Products_63131920/Delete/5
        public ActionResult Delete(int? id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Admin/Products_63131920/Delete/5

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
