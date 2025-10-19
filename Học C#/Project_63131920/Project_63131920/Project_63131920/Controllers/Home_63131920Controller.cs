using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Project_63131920.Context;
using Project_63131920.Models;

namespace Project_63131920.Controllers
{
    public class Home_63131920Controller : Controller
    {
        QLCH_63131920Entities objQLCHEntities = new QLCH_63131920Entities();
        public ActionResult Index()
        {
            HomeModel_63131920 objHomeModel = new HomeModel_63131920();
            objHomeModel.ListProduct = objQLCHEntities.Products.ToList();
            objHomeModel.ListCate = objQLCHEntities.Categories.Include(n => n.Products).ToList();
            return View(objHomeModel);
        }

        public ActionResult Introduce()
        {
            return View();
        }

        //GET: Register
        [HttpGet]

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objQLCHEntities.Users.FirstOrDefault(s => s.email == _user.email);
                if (check == null)
                {
                    objQLCHEntities.Configuration.ValidateOnSaveEnabled = false;
                    objQLCHEntities.Users.Add(_user);
                    objQLCHEntities.SaveChanges();
                    Session["FullName"] = _user.fullname;
                    Session["Email"] = _user.email;
                    Session["id"] = _user.id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();


        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            
            var data = objQLCHEntities.Admins.Where(s => s.email.Equals(email) && s.password.Equals(password));
            if (data.Count() > 0)
            {
                //add session
                Session["FullName"] = data.FirstOrDefault().fullname;
                Session["Email"] = data.FirstOrDefault().email;
                Session["id"] = data.FirstOrDefault().id;
                return RedirectToAction("Index", "Admin/Products_63131920");
            }
            var data2 = objQLCHEntities.Users.Where(s => s.email.Equals(email) && s.password.Equals(password));
            if (data2.Count() > 0)
            {
                //add session
                Session["FullName"] = data2.FirstOrDefault().fullname;
                Session["Email"] = data2.FirstOrDefault().email;
                Session["id"] = data2.FirstOrDefault().id;
                return RedirectToAction("Index");
            }
            ViewBag.error = "Tên đăng nhập hoặc mật khẩu không đúng";
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }
    }
}