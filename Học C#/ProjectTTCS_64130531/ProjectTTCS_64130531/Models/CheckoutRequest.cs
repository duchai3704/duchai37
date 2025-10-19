using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTTCS_64130531.Models
{
    public class CheckoutRequest
    {
        public UserH User { get; set; }
        public List<donmua> Ctdm { get; set; }
        public int TongTien { get; set; }
    }
}