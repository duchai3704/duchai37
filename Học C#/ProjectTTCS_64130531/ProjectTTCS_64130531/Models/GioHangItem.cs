using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTTCS_64130531.Models
{
    public class GioHangItem
    {
        public string IdCthd { get; set; }
        public string IdHD { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public string HinhAnh { get; set; }
        public Nullable<decimal> Gia { get; set; }
        public string TenSanPham { get; set; }
        public string Mau { get; set; }
        public int Soluongton { get; set; }
        public Nullable<double> Makhuyenmai { get; set; }


    }

}