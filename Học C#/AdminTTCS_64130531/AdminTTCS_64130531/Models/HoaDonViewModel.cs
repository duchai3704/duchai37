using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminTTCS_64130531.Models
{
    public class HoaDonViewModel
    {
        public string MaHoaDon { get; set; } // Mã hóa đơn
        public string TenKhachHang { get; set; } // Tên khách hàng
        public string DiaChiHD { get; set; } // Địa chỉ hóa đơn
        public Nullable<decimal> TongTienHD { get; set; } // Tổng tiền hóa đơn
        public string TrangThai { get; set; } // Trạng thái (VD: Đã giao, Chưa giao)
        public Nullable<System.DateTime> NgayTaoHD { get; set; } // Ngày tạo hóa đơn
        public Nullable<System.DateTime> NgayGiaoHD { get; set; } // Ngày giao hóa đơn (nullable, có thể là null nếu chưa giao)
    }
}