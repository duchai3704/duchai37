using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminTTCS_64130531.Models;

namespace AdminTTCS_64130531.Controllers
{
    public class HangSanXuatController : Controller
    {
        private readonly AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities _db = new AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities();

        // Hiển thị danh sách các danh mục
        public ActionResult Index()
        {
            try
            {
                // Lấy tất cả danh mục từ cơ sở dữ liệu
                List<Hangsanxuat> companys = _db.Hangsanxuat.ToList();

                // Gán danh sách hãng sản xuất vào ViewBag
                ViewBag.companys = companys;

                return View(companys);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        // Hiển thị form thêm hãng sản xuất
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // Xử lý form thêm danh mục
        [HttpPost]
        public ActionResult Create(Hangsanxuat model)
        {
            if (ModelState.IsValid)
            {
                // Thêm hãng sản sản xuất mới vào database
                _db.Hangsanxuat.Add(model);
                _db.SaveChanges();

                // Chuyển hướng về trang danh sách hãng sản xuất
                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, hiển thị lại form
            return View(model);
        }

        // Hiển thị form chỉnh sửa hãng sản xuất
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Tìm danh mục theo ID
            var company = _db.Hangsanxuat.FirstOrDefault(c => c.Mahang == id);
            if (company == null)
            {
                return HttpNotFound(); // Trả về 404 nếu không tìm thấy hãng sản xuất
            }
            // Truyền thông tin vào ViewBag để sử dụng trong view
            ViewBag.Mahang = company.Mahang;
            ViewBag.Tenhang = company.Tenhang;

            return View(company);
        }

        // Xử lý form chỉnh sửa hãng sản xuất
        [HttpPost]
        public ActionResult Edit(Hangsanxuat updatedcompany)
        {
            if (ModelState.IsValid)
            {
                var company = _db.Hangsanxuat.FirstOrDefault(c => c.Mahang == updatedcompany.Mahang);
                if (company == null)
                {
                    return HttpNotFound(); // Trả về 404 nếu không tìm thấy hãng sản xuất
                }

                // Cập nhật thông tin hãng sản xuất
                company.Tenhang = updatedcompany.Tenhang;

                // Lưu thay đổi vào database
                _db.SaveChanges();

                // Chuyển hướng về trang danh sách hãng sản xuất
                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, hiển thị lại form
            return View(updatedcompany);
        }

        // Xử lý xóa hãng sản xuất
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var company = _db.Hangsanxuat.FirstOrDefault(c => c.Mahang == id);
            if (company != null)
            {
                _db.Hangsanxuat.Remove(company);
                _db.SaveChanges(); // Lưu thay đổi vào database
            }

            return RedirectToAction("Index");
        }
    }
}
