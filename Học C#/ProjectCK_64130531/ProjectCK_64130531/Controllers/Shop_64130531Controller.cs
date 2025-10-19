using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProjectCK_64130531.Models;

namespace ProjectCK_64130531.Controllers
{
    public class Shop_64130531Controller : Controller
    {
        QLBanGiay_64130531Entities db = new QLBanGiay_64130531Entities();

        // GET: Shop_64130531
        public async Task<List<GioHangItem>> getGiohang(string idUser)
        {
            try
            {
                var result = await (from cthd in db.Chitiethoadon
                                    join cts in db.Chitietsanpham on cthd.id_chitietSP equals cts.id_chitietSP
                                    where cthd.Hoadon.taikhoan == idUser && cthd.Hoadon.id_Trangthai == 0
                                    select new GioHangItem
                                    {
                                        IdCthd = cthd.id_chitietHD,
                                        IdHD = cthd.Hoadon.maHoadon,
                                        SoLuong = cthd.soLuong,
                                        HinhAnh = cts.hinhAnh, // Thêm thuộc tính hình ảnh
                                        Gia = cts.gia,         // Thêm thuộc tính giá
                                        TenSanPham = cts.Sanpham.tenSanpham, // Thêm tên sản phẩm
                                        Mau = cts.tenMau,         // Thêm màu
                                        Soluongton = cts.soLuongTon, // Thêm số lượng tồn
                                        Makhuyenmai = cts.Sanpham.Khuyenmai.phanTram // Thêm mã khuyến mãi
                                    }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                // Trả về danh sách rỗng nếu xảy ra lỗi
                return new List<GioHangItem>();
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddCart(string userId, string idctsp)
        {
            try
            {
                var success = await TCart(userId, idctsp);

                if (success)
                {
                    return Json(new { success = true, message = "Đã thêm sản phẩm vào giỏ hàng." });
                }
                else
                {
                    return Json(new { success = false, message = "Thêm sản phẩm vào giỏ hàng thất bại." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Đã xảy ra lỗi: {ex.Message}" });
            }
        }

        public async Task<ActionResult> Cart(string idUser)
        {
            List<GioHangItem> list = await getGiohang(idUser);
            return View(list);

        }
        [HttpPost]
        public async Task<JsonResult> deleteProduct(List<string> idList)
        {
            try
            {
                if (idList == null || !idList.Any())
                {
                    return Json(new { success = false, message = "Danh sách sản phẩm cần xóa không hợp lệ." });
                }

                // Tìm các sản phẩm trong giỏ hàng dựa trên danh sách ID
                var itemsToDelete = db.Chitiethoadon.Where(cthd => idList.Contains(cthd.id_chitietHD)).ToList();

                if (!itemsToDelete.Any())
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm cần xóa." });
                }

                // Xóa các sản phẩm
                db.Chitiethoadon.RemoveRange(itemsToDelete);
                await db.SaveChangesAsync();

                return Json(new { success = true, message = "Xóa sản phẩm thành công." });
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                Console.WriteLine($"Error in deleteProduct: {ex.Message}");

                return Json(new { success = false, message = $"Đã xảy ra lỗi: {ex.Message}" });
            }
        }

        private string GenerateTimestampId()
        {
            var timestamp = DateTime.Now.ToString("yyMMddHH"); // 8 ký tự
            var random = new Random().Next(10, 99); // 2 ký tự
            return timestamp + random.ToString();
        }

        public async Task<bool> TCart(string idUser, string idctsp)
        {
            try
            {
                string newId;
                do
                {
                    newId = GenerateTimestampId(); // Tạo ID ngẫu nhiênZ
                } while (db.Chitiethoadon.Any(cthd => cthd.id_chitietHD == newId)); // Đảm bảo không trùng
                var newChitiethoadon = new Chitiethoadon
                {
                    id_chitietHD = newId,
                    maHoadon = idUser.Substring(idUser.Length - 1) + "0", // Thay thế mã hóa đơn nếu cần
                    id_chitietSP = idctsp,
                    soLuong = 1,
                    trangThai = 0 // Giá trị trạng thái (có thể null nếu không cần)
                };

                db.Chitiethoadon.Add(newChitiethoadon);
                await db.SaveChangesAsync();

                return true; // Trả về true nếu thêm thành công
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine(ex.Message);

                return false; // Trả về false nếu xảy ra lỗi
            }
        }
    }
}