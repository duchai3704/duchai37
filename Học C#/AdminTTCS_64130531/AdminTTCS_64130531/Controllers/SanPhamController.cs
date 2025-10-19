using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AdminTTCS_64130531.Models;

namespace AdminTTCS_64130531.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities _db = new AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities() ;
        public ActionResult Index(string searchQuery)
        {
            try
            {
                List<Sanpham> products = new List<Sanpham>();

                // Kiểm tra nếu có từ khóa tìm kiếm
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    // Tìm các sản phẩm chứa từ khóa trong tên sản phẩm
                    products = _db.Sanpham
                        .Where(p => p.tenSanpham.Contains(searchQuery))
                        .ToList();
                }
                else
                {
                    // Nếu không có từ khóa tìm kiếm, lấy tất cả sản phẩm
                    products = _db.Sanpham.ToList();
                }

                // Gán searchQuery vào ViewData để hiển thị trong View
                ViewData["SearchQuery"] = searchQuery;

                // Gán danh sách sản phẩm vào ViewBag
                ViewBag.Products = products;

                // Lấy danh sách khuyến mãi, hệ điều hành và hãng sản xuất
                var khuyenmais = _db.Khuyenmai.ToList();
                var hedieuhanhs = _db.Hedieuhanh.ToList();
                var hangsanxuats = _db.Hangsanxuat.ToList();

                // Gán danh sách vào ViewBag để truyền xuống View
                ViewBag.MaKhuyenmai = khuyenmais;
                ViewBag.mahdh = hedieuhanhs;
                ViewBag.mahang = hangsanxuats;

                // Trả về danh sách sản phẩm tìm được
                return View(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }


        [HttpPost]
        public ActionResult Edit( Sanpham update)
        {
            // Find the product from the database
            var sanpham = _db.Sanpham.FirstOrDefault(sp => sp.maSanpham == update.maSanpham);

            if (sanpham == null)
            {
                return HttpNotFound(); // Return 404 if product is not found
            }

            // Update the product fields with the data from the form (update)
            sanpham.tenSanpham = update.tenSanpham;
            sanpham.moTa = update.moTa;
            sanpham.soLuongyeuthich = update.soLuongyeuthich;
            sanpham.Mahdh = update.Mahdh;
            sanpham.Mahang = update.Mahang;
            sanpham.maKhuyenmai = update.maKhuyenmai;

            // Save changes to the database
            _db.SaveChanges();

            // Redirect to the product list or to the details page
            return RedirectToAction("Index"); // Change to your desired action
        }


        [HttpPost]
        public ActionResult Create(Sanpham model)
        {
            if (ModelState.IsValid)
            {
                using (var context = new AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities())
                {
                    // Thêm sản phẩm mới vào database
                    context.Sanpham.Add(model);
                    context.SaveChanges();
                }

                // Chuyển hướng về trang danh sách sản phẩm
                return RedirectToAction("Index");
            }

            // Nếu không hợp lệ, hiển thị lại form
            return View(model);
        }
        [HttpGet]
        public ActionResult Editsp(string id)
        {
            using (var context = new AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities())
            {
                // Lấy sản phẩm theo ID
                var sanpham = context.Sanpham.FirstOrDefault(sp => sp.maSanpham == id);
                if (sanpham == null)
                {
                    return null; // Trả về 404 nếu không tìm thấy sản phẩm
                }
                var khuyenmais = _db.Khuyenmai.ToList();
                var hedieuhanhs = _db.Hedieuhanh.ToList();
                var hangsanxuats = _db.Hangsanxuat.ToList();

                // Gán danh sách vào ViewBag để truyền xuống View
                ViewBag.MaKhuyenmai = khuyenmais;
                ViewBag.mahdh = hedieuhanhs;
                ViewBag.mahang = hangsanxuats;

                ViewBag.km= sanpham.Khuyenmai;
                ViewBag.hsx = sanpham.Hangsanxuat;
                ViewBag.hdh = sanpham.Hedieuhanh;
                @ViewBag.sp = sanpham;
                return View(); // Truyền sản phẩm vào view để hiển thị form
            }
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(string id)
        {
            using (var context = new AdminTTCS_64130531.Models.QLDienThoaiPDH_64130531Entities())
            {
                var sanpham = context.Sanpham.FirstOrDefault(sp => sp.maSanpham == id);
                if (sanpham != null)
                {
                    context.Sanpham.Remove(sanpham);
                    context.SaveChanges(); // Lưu thay đổi vào database
                }
                return RedirectToAction("Index");
            }
        }
    }
}
