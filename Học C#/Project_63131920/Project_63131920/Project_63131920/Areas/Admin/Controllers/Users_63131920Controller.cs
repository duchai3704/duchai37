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
    public class Users_63131920Controller : Controller
    {
        private QLCH_63131920Entities db = new QLCH_63131920Entities();

        // GET: Admin/Users_63131920
        public ActionResult Index(string user_search = "")
        {
            string cmd = "SELECT * FROM \"User\"";
            if (user_search != "")
            {
                cmd = "SELECT * FROM \"User\" WHERE fullname LIKE N'%" +user_search+ "%' or phone_number = '" +user_search+ "' ";
            }
            var users = db.Users.SqlQuery(cmd);
            if(users.Count() == 0)
            { ViewBag.Alert = "Không tìm thấy thông tin khách hàng";}
            return View(users);
        }

        // GET: Admin/Users_63131920/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users_63131920/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users_63131920/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,fullname,email,phone_number,password,sex")] User user)
        {
            var existingUser = db.Users.FirstOrDefault(m => m.phone_number == user.phone_number);

            if (existingUser != null)
            {
                ModelState.AddModelError("phone_number", "Số điện thoại đã tồn tại. Vui lòng chọn số điện thoại khác.");
            }
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Users_63131920/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users_63131920/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,fullname,email,phone_number,password,sex")] User user)
        {

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users_63131920/Delete/5
        public ActionResult Delete(int? id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Admin/Users_63131920/Delete/5

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
