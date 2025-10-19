using Project_63131920.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_63131920.Models
{
    public class mapProduct_63131920
    {
        public List<Product> List()
        {
            var db = new QLCH_63131920Entities();
            var data = db.Products.ToList();
            return data;
        }
    }
}