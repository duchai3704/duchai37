using Project_63131920.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_63131920.Controllers
{
    public class Product_63131920Controller : Controller
    {
        QLCH_63131920Entities objQLCHEntities = new QLCH_63131920Entities();
        // GET: Product_63131920
        public ActionResult Detail(int Id)
        {
            var objProduct = objQLCHEntities.Products.Where(n => n.id == Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}