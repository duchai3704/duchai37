using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Admin_64130531.Models;
namespace Admin_64130531.Controllers
{
    public class KhuyenMaiController : Controller
    {
        private readonly Admin_64130531.Models.QLBanGiay_64130531Entities _db = new Admin_64130531.Models.QLBanGiay_64130531Entities();

        // Hiển thị danh sách các khuyến mãi
        public ActionResult Index()
        {
            try
            {
                // Lấy tất cả khuyến mãi từ cơ sở dữ liệu
                List<Khuyenmai> promotions = _db.Khuyenmai.ToList();

                // Gán danh sách khuyến mãi vào ViewBag
                ViewBag.promotions = promotions;

                return View(promotions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        // Hiển thị form thêm khuyến mãi
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // Xử lý form thêm khuyến mãi
        [HttpPost]
        public ActionResult Create(Khuyenmai model)
        {
            if (ModelState.IsValid)
            {
                // Thêm khuyến mãi mới vào database
                _db.Khuyenmai.Add(model);
                _db.SaveChanges();

                // Chuyển hướng về trang danh sách khuyến mãi
                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, hiển thị lại form
            return View(model);
        }

        // Hiển thị form chỉnh sửa khuyến mãi
        [HttpGet]
        public ActionResult Edit(string id)
        {
            // Tìm khuyến mãi theo ID
            var promotion = _db.Khuyenmai.FirstOrDefault(c => c.maKhuyenmai == id);
            if (promotion == null)
            {
                return HttpNotFound(); // Trả về 404 nếu không tìm thấy khuyến mãi
            }
            // Truyền thông tin vào ViewBag để sử dụng trong view
            ViewBag.maKhuyenmai = promotion.maKhuyenmai;
            ViewBag.tenKhuyenmai = promotion.tenKhuyenmai;
            ViewBag.phanTram = promotion.phanTram;

            return View(promotion);
        }

        // Xử lý form chỉnh sửa khuyến mãi
        [HttpPost]
        public ActionResult Edit(Khuyenmai updatedPromotion)
        {
            if (ModelState.IsValid)
            {
                var promotion = _db.Khuyenmai.FirstOrDefault(c => c.maKhuyenmai == updatedPromotion.maKhuyenmai);
                if (promotion == null)
                {
                    return HttpNotFound(); // Trả về 404 nếu không tìm thấy khuyến mãi
                }

                // Cập nhật thông tin khuyến mãi
                promotion.tenKhuyenmai = updatedPromotion.tenKhuyenmai;
                promotion.phanTram = updatedPromotion.phanTram;

                // Lưu thay đổi vào database
                _db.SaveChanges();

                // Chuyển hướng về trang danh sách khuyến mãi
                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, hiển thị lại form
            return View(updatedPromotion);
        }

        // Xử lý xóa khuyến mãi
        [HttpPost]
        public ActionResult DeleteConfirmed(string id)
        {
            var promotion = _db.Khuyenmai.FirstOrDefault(c => c.maKhuyenmai == id);
            if (promotion != null)
            {
                _db.Khuyenmai.Remove(promotion);
                _db.SaveChanges(); // Lưu thay đổi vào database
            }

            return RedirectToAction("Index");
        }
    }
}
