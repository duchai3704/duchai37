using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Admin_64130531.Models;

namespace Admin_64130531.Controllers
{
    public class ChiTietSanPhamController : Controller
    {
        private readonly Admin_64130531.Models.QLBanGiay_64130531Entities _db = new Admin_64130531.Models.QLBanGiay_64130531Entities();

        // Hiển thị danh sách chi tiết sản phẩm
        public ActionResult Index()
        {
            try
            {
                // Lấy tất cả chi tiết sản phẩm từ cơ sở dữ liệu
                List<Chitietsanpham> productDetails = _db.Chitietsanpham.ToList();

                var sanphams = _db.Sanpham.ToList();
                var colors = _db.Color.ToList();
                var sizes = _db.Size.ToList();

                // Gán danh sách vào ViewBag để truyền xuống View
                ViewBag.tenSanpham = sanphams;
                ViewBag.maMau = colors;
                ViewBag.maSize = sizes;

                // Gán danh sách chi tiết sản phẩm vào ViewBag
                ViewBag.ProductDetails = productDetails;

                return View(productDetails);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        // Hiển thị form thêm chi tiết sản phẩm
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // Xử lý form thêm chi tiết sản phẩm
        [HttpPost]
        public ActionResult Create(Chitietsanpham model)
        {
            if (ModelState.IsValid)
            {
                // Thêm chi tiết sản phẩm mới vào database
                _db.Chitietsanpham.Add(model);
                _db.SaveChanges();

                // Chuyển hướng về trang danh sách chi tiết sản phẩm
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Hiển thị form chỉnh sửa chi tiết sản phẩm
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
            var sizes = _db.Size.ToList();

            // Gán danh sách vào ViewBag để truyền xuống View
            ViewBag.id_chitietSP = id;
            ViewBag.tenSanpham = sanphams;
            ViewBag.maMau = colors;
            ViewBag.maSize = sizes;
            ViewBag.hinhAnh = productDetail.hinhAnh;

            return View(productDetail);
        }

        // Xử lý form chỉnh sửa chi tiết sản phẩm
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
                productDetail.maSize = updatedProductDetail.maSize;
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
        // Xử lý xóa chi tiết sản phẩm
        [HttpPost]
        public ActionResult DeleteConfirmed(string id)
        {
            // Tìm chi tiết sản phẩm theo ID
            var productDetail = _db.Chitietsanpham.FirstOrDefault(ct => ct.id_chitietSP == id);
            if (productDetail != null)
            {
                // Xóa chi tiết sản phẩm
                _db.Chitietsanpham.Remove(productDetail);
                _db.SaveChanges(); // Lưu thay đổi vào database
            }

            // Chuyển hướng về trang danh sách chi tiết sản phẩm
            return RedirectToAction("Index");
        }
    }
}
