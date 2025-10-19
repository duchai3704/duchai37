using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTTCS_64130531.Models
{
    public class HoadonDetailK
    {
        public string IdChitietHd { get; set; }
        public string HinhAnh { get; set; }
        public string Bonhotrong { get; set; }
        public string TenSanpham { get; set; }
        public Nullable<int> SoLuongMua { get; set; }
        public string Mau { get; set; }
        public Nullable<decimal> Gia { get; set; }
        public decimal TongTien { get; set; }
        public DateTime? NgayGiao { get; set; }
        public string DiaChi { get; set; }
        public string IdTrangthai { get; set; }
        public string MaSanpham { get; set; }
        public int MaMau { get; set; }
        public string idchitiet { get; set; }
    }
}