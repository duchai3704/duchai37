using Project_63131920.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_63131920.Controllers
{
    public class Category_63131920Controller : Controller
    {
        QLCH_63131920Entities objQLCHEntities = new QLCH_63131920Entities();
        // GET: Category_63131920
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objQLCHEntities.Products.Where(n => n.category_id == Id).ToList();
            return View(listProduct);        
        }
    }
}