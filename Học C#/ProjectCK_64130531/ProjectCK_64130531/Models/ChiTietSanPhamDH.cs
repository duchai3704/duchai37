using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCK_64130531.Models
{
    public class ChiTietSanPhamDH
    {
        public string ChitietId { get; set; }

        public string HinhAnh { get; set; }

        public int SoLuongCon { get; set; }
        public Nullable<decimal> Gia { get; set; }
        public int mamau { get; set; }
        public int masize { get; set; }
        public string TenSanPham { get; set; }
        public string MauSac { get; set; }
        public string MaSanPham { get; set; }
    }
}