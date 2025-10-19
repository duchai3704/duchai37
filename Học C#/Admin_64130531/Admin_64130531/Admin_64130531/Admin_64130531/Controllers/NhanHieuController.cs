using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Admin_64130531.Models;

namespace Admin_64130531.Controllers
{
    public class NhanHieuController : Controller
    {
        private readonly Admin_64130531.Models.QLBanGiay_64130531Entities _db = new Admin_64130531.Models.QLBanGiay_64130531Entities();

        // Hiển thị danh sách các nhãn hiệu
        public ActionResult Index()
        {
            try
            {
                // Lấy tất cả nhãn hiệu từ cơ sở dữ liệu
                List<Nhanhieu> brands = _db.Nhanhieu.ToList();

                // Gán danh sách nhãn hiệu vào ViewBag
                ViewBag.Brands = brands;

                return View(brands);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        // Hiển thị form thêm nhãn hiệu
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // Xử lý form thêm nhãn hiệu
        [HttpPost]
        public ActionResult Create(Nhanhieu model)
        {
            if (ModelState.IsValid)
            {
                // Thêm nhãn hiệu mới vào database
                _db.Nhanhieu.Add(model);
                _db.SaveChanges();

                // Chuyển hướng về trang danh sách nhãn hiệu
                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, hiển thị lại form
            return View(model);
        }

        // Hiển thị form chỉnh sửa nhãn hiệu
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Tìm nhãn hiệu theo ID
            var brand = _db.Nhanhieu.FirstOrDefault(c => c.maNhan == id);
            if (brand == null)
            {
                return HttpNotFound(); // Trả về 404 nếu không tìm thấy nhãn hiệu
            }
            // Truyền thông tin vào ViewBag để sử dụng trong view
            ViewBag.maNhan = brand.maNhan;
            ViewBag.tenNhan = brand.tenNhan;

            return View(brand);
        }

        // Xử lý form chỉnh sửa nhãn hiệu
        [HttpPost]
        public ActionResult Edit(Nhanhieu updatedBrand)
        {
            if (ModelState.IsValid)
            {
                var brand = _db.Nhanhieu.FirstOrDefault(c => c.maNhan == updatedBrand.maNhan);
                if (brand == null)
                {
                    return HttpNotFound(); // Trả về 404 nếu không tìm thấy nhãn hiệu
                }

                // Cập nhật thông tin nhãn hiệu
                brand.tenNhan = updatedBrand.tenNhan;

                // Lưu thay đổi vào database
                _db.SaveChanges();

                // Chuyển hướng về trang danh sách nhãn hiệu
                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, hiển thị lại form
            return View(updatedBrand);
        }

        // Xử lý xóa nhãn hiệu
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var brand = _db.Nhanhieu.FirstOrDefault(c => c.maNhan == id);
            if (brand != null)
            {
                _db.Nhanhieu.Remove(brand);
                _db.SaveChanges(); // Lưu thay đổi vào database
            }

            return RedirectToAction("Index");
        }
    }
}
