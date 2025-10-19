using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProjectCK_64130531.Models;

namespace ProjectCK_64130531.Controllers
{
    public class Home_64130531Controller : Controller
    {
        QLBanGiay_64130531Entities db = new QLBanGiay_64130531Entities();
        // GET: Home_64130531
        public ActionResult Index(string searchQuery)
        {
            // Kiểm tra nếu có từ khóa tìm kiếm
            var list = from ctp in db.Chitietsanpham
                       join sp in db.Sanpham on ctp.maSanpham equals sp.maSanpham
                       join nh in db.Nhanhieu on sp.maNhan equals nh.maNhan
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
                           TenNhan = nh.tenNhan,
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
    }
}
