using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Admin_64130531.Models;

namespace Admin_64130531.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly Admin_64130531.Models.QLBanGiay_64130531Entities _db;

        public HoaDonController()
        {
            _db = new Admin_64130531.Models.QLBanGiay_64130531Entities();
        }
        public ActionResult Index()
        {
            // Lấy danh sách từ cơ sở dữ liệu
            var dshd = _db.Hoadon.Include("Trangthai").ToList();

            var trangthais = _db.Trangthai.ToList();
            ViewBag.TrangThai = trangthais;

            // Ánh xạ sang danh sách ViewModel
            var danhSachHoaDon = dshd.Select(hd => new HoaDonViewModel
            {
                MaHoaDon = hd.maHoadon,
                DiaChiHD = hd.diaChi,
                NgayTaoHD = hd.ngayTao,
                NgayGiaoHD = hd.ngayGiao,
                TrangThai = hd.Trangthai?.tenTrangthai ?? "Chưa cập nhật", // Nếu có bảng trạng thái
                TongTienHD = hd.tongTien,
                TenKhachHang = hd.Users.ten
            }).ToList();

            // Truyền dữ liệu sang view
            return View(danhSachHoaDon);
        }


        // API: Lấy chi tiết hóa đơn theo mã hóa đơn
        [HttpGet]
        public JsonResult GetHoaDonDetails(string maHoaDon)
        {
            var chiTietHoaDon = _db.Chitiethoadon
                .Where(ct => ct.maHoadon == maHoaDon)
                .Select(ct => new
                {
                    tenSP = ct.Chitietsanpham.Sanpham.tenSanpham,
                    thuongHieu = ct.Chitietsanpham.Sanpham.Nhanhieu.tenNhan,
                    giaSP = ct.Chitietsanpham.gia,
                    soLuongSP = ct.soLuong,
                    mau = ct.Chitietsanpham.Color.tenMau,
                    kichThuoc = ct.Chitietsanpham.Size.maSize,
                    khuyenMai = ct.Chitietsanpham.Sanpham.Khuyenmai.tenKhuyenmai,
                    tongGia = ct.Chitietsanpham.gia
                }).ToList();

            return Json(chiTietHoaDon, JsonRequestBehavior.AllowGet);
        }

        // API: Cập nhật trạng thái hóa đơn
        [HttpPost]
        public JsonResult UpdateStatus(string maHoaDon, DateTime ngayCapNhat, int trangThai)
        {
            try
            {
                var hoaDon = _db.Hoadon.FirstOrDefault(hd => hd.maHoadon == maHoaDon);

                if (hoaDon == null)
                {
                    return Json(new { success = false, message = "Hóa đơn không tồn tại" });
                }

                // Cập nhật trạng thái và ngày giao (nếu có)
                hoaDon.id_Trangthai = trangThai;
                hoaDon.ngayGiao = ngayCapNhat; // Nếu cần cập nhật ngày giao

                // Lưu thay đổi vào cơ sở dữ liệu
                _db.SaveChanges();

                return Json(new { success = true, message = "Cập nhật trạng thái thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}