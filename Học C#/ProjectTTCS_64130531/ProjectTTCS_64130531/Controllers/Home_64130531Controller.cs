using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Newtonsoft.Json;
using ProjectTTCS_64130531.Models;

namespace ProjectTTCS_64130531.Controllers
{
    public class Home_64130531Controller : Controller
    {
        QLDienThoaiPDH_64130531Entities db = new QLDienThoaiPDH_64130531Entities();
        // GET: Home_64130531
        public ActionResult Index(string searchQuery)
        {
            // Kiểm tra nếu có từ khóa tìm kiếm
            var list = from ctp in db.Chitietsanpham
                       join sp in db.Sanpham on ctp.maSanpham equals sp.maSanpham
                       join nh in db.Hedieuhanh on sp.Mahdh equals nh.Mahdh
                       join cl in db.Color on ctp.maMau equals cl.maMau
                       where string.IsNullOrEmpty(searchQuery) || sp.tenSanpham.Contains(searchQuery)
                       select new Category
                       {
                           IdChitietSP = ctp.id_chitietSP,
                           HinhAnh = ctp.hinhAnh,
                           SoLuongTon = ctp.soLuongTon,
                           Gia = ctp.gia,
                           MaMau = ctp.maMau,
                           TenSanpham = sp.tenSanpham,
                           TenNhan = nh.Tenhdh,
                           TenMau = cl.tenMau,
                       };

            // Gán danh sách sản phẩm tìm được vào ViewBag
            ViewBag.SanphamList = list.ToList();

            // Gán từ khóa tìm kiếm vào ViewData để hiển thị trên View
            ViewData["SearchQuery"] = searchQuery;

            return View();
        }
        public ActionResult Chitietsanpham(string id)
        {
            var chitiet = (from ctp in db.Chitietsanpham
                           join sp in db.Sanpham on ctp.maSanpham equals sp.maSanpham
                           where ctp.id_chitietSP == id
                           select new Category
                           {
                               IdChitietSP = ctp.id_chitietSP,
                               HinhAnh = ctp.hinhAnh,
                               SoLuongTon = ctp.soLuongTon,
                               Gia = ctp.gia,
                               MaMau = ctp.maMau,
                               TenSanpham = sp.tenSanpham,
                               MoTa = sp.moTa
                           }).FirstOrDefault();

            if (chitiet == null)
            {
                return HttpNotFound(); // Trả về lỗi nếu không tìm thấy sản phẩm
            }

            return View(chitiet);
        }
        public JsonResult Search(string key)
        {
            try
            {
                key = key?.Trim('\0').Trim(); // Cắt ký tự null và khoảng trắng

                var result = db.Chitietsanpham
                    .Join(db.Sanpham, cts => cts.maSanpham, sp => sp.maSanpham, (cts, sp) => new { cts, sp })
                    .Join(db.Color, temp => temp.cts.maMau, mau => mau.maMau, (temp, mau) => new { temp.cts, temp.sp, mau })
.Where(x => x.sp.tenSanpham.StartsWith(key))
                    .Select(x => new ChiTietSanPhamDH
                    {
                        ChitietId = x.cts.id_chitietSP,
                        HinhAnh = x.cts.hinhAnh,
                        SoLuongCon = x.cts.soLuongTon,
                        Gia = x.cts.gia,
                        mamau = x.cts.maMau,
                        bonhotrong = x.cts.Dungluong,
                        TenSanPham = x.sp.tenSanpham,
                        MauSac = x.mau.tenMau,
                        MaSanPham = x.sp.maSanpham
                    }).ToList();

                // Trả về danh sách sản phẩm dưới dạng JSON
                return Json(new { data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có sự cố
                Console.WriteLine(ex.Message);

                // Trả về thông báo lỗi dưới dạng JSON
                return Json(new { error = true, message = "Đã xảy ra lỗi khi lấy danh sách sản phẩm." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
