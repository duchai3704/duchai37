using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCK_64130531.Models
{
    public class HoadonK
    {
        public string MaHoadon { get; set; }
        public string DiaChi { get; set; }
        public DateTime? NgayTao { get; set; }
        public DateTime? NgayGiao { get; set; }
        public int? IdTrangthai { get; set; }
        public decimal? TongTien { get; set; }
        public string Taikhoan { get; set; }
        public string TenTrangthai { get; set; }
    }
}