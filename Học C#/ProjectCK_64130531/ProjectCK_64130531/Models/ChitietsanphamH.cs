using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ProjectCK_64130531.Models
{
    public class ChitietsanphamH
    {
        public string IdChitietSp { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuongTon { get; set; }
        public decimal Gia { get; set; }
        public string TenMau { get; set; }
    }
}