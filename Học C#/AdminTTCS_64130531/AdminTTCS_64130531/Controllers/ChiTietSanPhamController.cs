using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminTTCS_64130531.Models;

namespace AdminTTCS_64130531.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        private readonly AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities _db = new AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities();

        // Hiển thị danh sách chi tiết sản phẩm điện thoại
        public ActionResult Index(string searchQuery)
        {
            try
            {
                List<Chitietsanpham> details = new List<Chitietsanpham>();

                // Kiểm tra nếu có từ khóa tìm kiếm
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    details = _db.Chitietsanpham
                        .Where(ct => ct.Sanpham.tenSanpham.Contains(searchQuery))
                        .ToList();
                }
                else
                {
                    details = _db.Chitietsanpham.ToList();
                }

                // Gán searchQuery vào ViewData để hiển thị trong View
                ViewData["SearchQuery"] = searchQuery;

                // Lấy danh sách màu sắc, dung lượng và sản phẩm
                var colors = _db.Color.ToList();
                var memorySizes = _db.BoNhoTrong.ToList();
                var sanphams = _db.Sanpham.ToList();

                // Gán danh sách vào ViewBag để truyền xuống View
                ViewBag.maMau = colors;
                ViewBag.id_dungluong = memorySizes;
                ViewBag.tenSanpham = sanphams;
                ViewBag.maSanpham = sanphams;

                return View(details);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        // Tạo chi tiết sản phẩm điện thoại mới
        [HttpGet]
        public ActionResult Create(string maSanpham)
        {
            var model = new Chitietsanpham();
            using (var context = new AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities())
            {
                // Lấy thông tin sản phẩm từ mã sản phẩm
                var sanpham = context.Sanpham.FirstOrDefault(sp => sp.maSanpham == maSanpham);
                if (sanpham == null)
                {
                    return HttpNotFound(); // Nếu không tìm thấy sản phẩm
                }

                model.maSanpham = maSanpham;

                // Lấy thông tin màu sắc, dung lượng
                ViewBag.Mau = new SelectList(context.Color, "maMau", "tenMau");
                ViewBag.DungLuong = new SelectList(context.BoNhoTrong, "id_dungluong", "Dungluong");
            }

            return View(model);
        }

        // Xử lý lưu chi tiết sản phẩm điện thoại mới
        [HttpPost]
        public ActionResult CreateSP(Chitietsanpham model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities())
                {
                    // Thêm chi tiết sản phẩm vào database
                    context.Chitietsanpham.Add(model);
                    context.SaveChanges();
                }

                // Chuyển hướng về trang danh sách chi tiết sản phẩm
                return RedirectToAction("Index");
            }

            return View(model); // Nếu không hợp lệ, hiển thị lại form
        }

        // Sửa chi tiết sản phẩm điện thoại
        [HttpGet]
        public ActionResult Edit(string id)
        {
            // Tìm chi tiết sản phẩm theo ID
            var productDetail = _db.Chitietsanpham.FirstOrDefault(ct => ct.id_chitietSP == id);
            if (productDetail == null)
            {
                return HttpNotFound(); // Trả về 404 nếu không tìm thấy chi tiết sản phẩm
            }

            var sanphams = _db.Sanpham.ToList();
            var colors = _db.Color.ToList();
            var bonhotrongs = _db.BoNhoTrong.ToList();

            // Gán danh sách vào ViewBag để truyền xuống View
            ViewBag.id_chitietSP = id;
            ViewBag.tenSanpham = sanphams;
            ViewBag.maMau = colors;
            ViewBag.Dungluong = bonhotrongs;
            ViewBag.hinhAnh = productDetail.hinhAnh;

            return View(productDetail);
        }

        // Lưu thay đổi chi tiết sản phẩm điện thoại
        [HttpPost]
        public JsonResult Edit(Chitietsanpham updatedProductDetail)
        {
            try
            {
                // Tìm chi tiết sản phẩm theo ID
                var productDetail = _db.Chitietsanpham.FirstOrDefault(ct => ct.id_chitietSP == updatedProductDetail.id_chitietSP);
                if (productDetail == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy chi tiết sản phẩm." });
                }

                // Cập nhật thông tin chi tiết sản phẩm
                productDetail.hinhAnh = updatedProductDetail.hinhAnh;
                productDetail.maSanpham = updatedProductDetail.maSanpham; // Chú ý chỉnh đúng cột maSanpham trong CSDL
                productDetail.maMau = updatedProductDetail.maMau;
                productDetail.Dungluong = updatedProductDetail.Dungluong;
                productDetail.soLuongTon = updatedProductDetail.soLuongTon;
                productDetail.gia = updatedProductDetail.gia;

                // Lưu thay đổi vào database
                _db.SaveChanges();

                return Json(new { success = true, message = "Cập nhật chi tiết sản phẩm thành công!" });
            }
            catch (Exception ex)
            {
                // Gửi lỗi chi tiết nếu cần
                return Json(new { success = false, message = "Đã xảy ra lỗi. Chi tiết: " + ex.Message });
            }
        }

        // Xóa chi tiết sản phẩm điện thoại
        [HttpPost]
        public ActionResult DeleteConfirmed(string id)
        {
            using (var context = new AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities())
            {
                var chitietsanpham = context.Chitietsanpham.FirstOrDefault(ct => ct.id_chitietSP == id);
                if (chitietsanpham != null)
                {
                    context.Chitietsanpham.Remove(chitietsanpham);
                    context.SaveChanges(); // Lưu thay đổi vào database
                }

                return RedirectToAction("Index");
            }
        }
    }
}
