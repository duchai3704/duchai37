using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin_64130531.Models
{
    public class BestSellingProduct
    {
        public string ProductId { get; set; }
        public int TotalQuantitySold { get; set; }
        public string ProductName { get; set; }
        public decimal? ProductPrice { get; set; }
    }
}