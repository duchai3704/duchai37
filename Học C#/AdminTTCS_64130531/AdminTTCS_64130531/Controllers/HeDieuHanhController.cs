using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminTTCS_64130531.Models;

namespace AdminTTCS_64130531.Controllers
{
    public class HeDieuHanhController : Controller
    {
        private readonly AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities _db = new AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities();

        // Hiển thị danh sách các hệ điều hành
        public ActionResult Index()
        {
            try
            {
                // Lấy tất cả hệ điều hành từ cơ sở dữ liệu
                List<Hedieuhanh> operatingSystems = _db.Hedieuhanh.ToList();

                // Gán danh sách hệ điều hành vào ViewBag
                ViewBag.OperatingSystems = operatingSystems;

                return View(operatingSystems);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        // Hiển thị form thêm hệ điều hành
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // Xử lý form thêm hệ điều hành
        [HttpPost]
        public ActionResult Create(Hedieuhanh model)
        {
            if (ModelState.IsValid)
            {
                // Thêm hệ điều hành mới vào database
                _db.Hedieuhanh.Add(model);
                _db.SaveChanges();

                // Chuyển hướng về trang danh sách hệ điều hành
                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, hiển thị lại form
            return View(model);
        }

        // Hiển thị form chỉnh sửa hệ điều hành
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Tìm hệ điều hành theo ID
            var operatingSystem = _db.Hedieuhanh.FirstOrDefault(os => os.Mahdh == id);
            if (operatingSystem == null)
            {
                return HttpNotFound(); // Trả về 404 nếu không tìm thấy hệ điều hành
            }

            // Truyền thông tin vào ViewBag để sử dụng trong view
            ViewBag.Mahdh = operatingSystem.Mahdh;
            ViewBag.Tenhdh = operatingSystem.Tenhdh;

            return View(operatingSystem);
        }

        // Xử lý form chỉnh sửa hệ điều hành
        [HttpPost]
        public ActionResult Edit(Hedieuhanh updatedOperatingSystem)
        {
            if (ModelState.IsValid)
            {
                var operatingSystem = _db.Hedieuhanh.FirstOrDefault(os => os.Mahdh == updatedOperatingSystem.Mahdh);
                if (operatingSystem == null)
                {
                    return HttpNotFound(); // Trả về 404 nếu không tìm thấy hệ điều hành
                }

                // Cập nhật thông tin hệ điều hành
                operatingSystem.Tenhdh = updatedOperatingSystem.Tenhdh;

                // Lưu thay đổi vào database
                _db.SaveChanges();

                // Chuyển hướng về trang danh sách hệ điều hành
                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, hiển thị lại form
            return View(updatedOperatingSystem);
        }

        // Xử lý xóa hệ điều hành
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var operatingSystem = _db.Hedieuhanh.FirstOrDefault(os => os.Mahdh == id);
            if (operatingSystem != null)
            {
                _db.Hedieuhanh.Remove(operatingSystem);
                _db.SaveChanges(); // Lưu thay đổi vào database
            }

            return RedirectToAction("Index");
        }
    }
}
