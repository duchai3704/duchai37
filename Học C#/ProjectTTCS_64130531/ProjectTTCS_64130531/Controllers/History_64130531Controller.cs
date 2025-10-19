using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ProjectTTCS_64130531.Models;

namespace ProjectTTCS_64130531.Controllers
{
    public class History_64130531Controller : Controller
    {
        QLDienThoaiPDH_64130531Entities db = new QLDienThoaiPDH_64130531Entities();

        // Lấy lịch sử hóa đơn theo tài khoản
        public async Task<ActionResult> History()
        {
            // Kiểm tra đăng nhập
            var idUser = Request.Cookies["idUser"]?.Value;
            if (string.IsNullOrEmpty(idUser))
            {
                TempData["ERROR"] = "Bạn phải đăng nhập để sử dụng tính năng này.";
                return RedirectToAction("Login", "Account_64130531");
            }

            try
            {
                // Lấy danh sách hóa đơn theo tài khoản
                var historyList = db.Hoadon
                    .Where(hd => hd.taikhoan == idUser)
                    .OrderByDescending(hd => hd.ngayTao)
                    .ToList();

                // Chuyển đổi sang danh sách HoadonK
                var historyListK = historyList.Select(h => new HoadonK
                {
                    MaHoadon = h.maHoadon,
                    NgayTao = h.ngayTao,
                    NgayGiao = h.ngayGiao,
                    DiaChi = h.diaChi,
                    TongTien = h.tongTien,
                    IdTrangthai = h.id_Trangthai
                }).ToList();

                // Lấy chi tiết hóa đơn với trạng thái 0 và 1 và thêm thông tin HinhAnh
                var historyListDt0 = db.Chitiethoadon
                    .Where(cthd => cthd.Hoadon.taikhoan == idUser && cthd.Hoadon.id_Trangthai == 0)
                    .Select(cthd => new
                    {
                        cthd,
                        HinhAnh = cthd.Chitietsanpham.hinhAnh  // Lấy thông tin hình ảnh từ Sanpham
                    })
                    .ToList();

                var historyListDt1 = db.Chitiethoadon
                    .Where(cthd => cthd.Hoadon.taikhoan == idUser && cthd.Hoadon.id_Trangthai == 1)
                    .Select(cthd => new
                    {
                        cthd,
                        HinhAnh = cthd.Chitietsanpham.hinhAnh // Lấy thông tin hình ảnh từ Sanpham
                    })
                    .ToList();


                // Đưa dữ liệu vào ViewBag
                ViewBag.trangthai0 = historyListDt0;
                ViewBag.trangthai1 = historyListDt1;

                return View(historyListK);
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Đã xảy ra lỗi: {ex.Message}";
                return RedirectToAction("Index", "Home_64130531");
            }
        }

        public async Task<ActionResult> HistoryDetail(string mahd)
        {
            var idUser = Request.Cookies["idUser"]?.Value;
            if (string.IsNullOrEmpty(idUser))
            {
                TempData["ERROR"] = "Bạn phải đăng nhập để sử dụng tính năng này.";
                return RedirectToAction("Login", "Account_64130531");
            }

            try
            {
                // Lấy danh sách chi tiết hóa đơn theo mã hóa đơn
                var model = await db.Chitiethoadon
                .Where(cthd => cthd.maHoadon == mahd)
                .Select(cthd => new HoadonDetailK
                {
                    TenSanpham = cthd.Chitietsanpham.Sanpham.tenSanpham,
                    Bonhotrong = cthd.Chitietsanpham.Dungluong,
                    Mau = cthd.Chitietsanpham.Color.tenMau,
                    SoLuongMua = cthd.soLuong,
                    Gia = (decimal?)cthd.Chitietsanpham.gia,
                    NgayGiao = cthd.Hoadon.ngayGiao,
                    DiaChi = cthd.Hoadon.diaChi,
                    IdTrangthai = cthd.Hoadon.Trangthai.tenTrangthai,
                    HinhAnh = cthd.Chitietsanpham.hinhAnh
                })
                .ToListAsync();
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Đã xảy ra lỗi khi tải dữ liệu: {ex.Message}";
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
