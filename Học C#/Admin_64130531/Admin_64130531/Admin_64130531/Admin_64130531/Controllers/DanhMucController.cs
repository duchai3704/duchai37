using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Admin_64130531.Models;

namespace Admin_64130531.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly Admin_64130531.Models.QLBanGiay_64130531Entities _db = new Admin_64130531.Models.QLBanGiay_64130531Entities();

        // Hiển thị danh sách các danh mục
        public ActionResult Index()
        {
            try
            {
                // Lấy tất cả danh mục từ cơ sở dữ liệu
                List<Danhmuc> categories = _db.Danhmuc.ToList();

                // Gán danh sách danh mục vào ViewBag
                ViewBag.Categories = categories;

                return View(categories);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        // Hiển thị form thêm danh mục
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // Xử lý form thêm danh mục
        [HttpPost]
        public ActionResult Create(Danhmuc model)
        {
            if (ModelState.IsValid)
            {
                // Thêm danh mục mới vào database
                _db.Danhmuc.Add(model);
                _db.SaveChanges();

                // Chuyển hướng về trang danh sách danh mục
                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, hiển thị lại form
            return View(model);
        }

        // Hiển thị form chỉnh sửa danh mục
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Tìm danh mục theo ID
            var category = _db.Danhmuc.FirstOrDefault(c => c.maDanhmuc == id);
            if (category == null)
            {
                return HttpNotFound(); // Trả về 404 nếu không tìm thấy danh mục
            }
            // Truyền thông tin vào ViewBag để sử dụng trong view
            ViewBag.maDanhmuc = category.maDanhmuc;
            ViewBag.tenDanhmuc = category.tenDanhmuc;

            return View(category);
        }

        // Xử lý form chỉnh sửa danh mục
        [HttpPost]
        public ActionResult Edit(Danhmuc updatedCategory)
        {
            if (ModelState.IsValid)
            {
                var category = _db.Danhmuc.FirstOrDefault(c => c.maDanhmuc == updatedCategory.maDanhmuc);
                if (category == null)
                {
                    return HttpNotFound(); // Trả về 404 nếu không tìm thấy danh mục
                }

                // Cập nhật thông tin danh mục
                category.tenDanhmuc = updatedCategory.tenDanhmuc;

                // Lưu thay đổi vào database
                _db.SaveChanges();

                // Chuyển hướng về trang danh sách danh mục
                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, hiển thị lại form
            return View(updatedCategory);
        }

        // Xử lý xóa danh mục
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var category = _db.Danhmuc.FirstOrDefault(c => c.maDanhmuc == id);
            if (category != null)
            {
                _db.Danhmuc.Remove(category);
                _db.SaveChanges(); // Lưu thay đổi vào database
            }

            return RedirectToAction("Index");
        }
    }
}
