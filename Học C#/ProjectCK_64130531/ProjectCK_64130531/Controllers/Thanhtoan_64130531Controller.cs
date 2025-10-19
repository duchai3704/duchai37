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
    public class Thanhtoan_64130531Controller : Controller
    {
        QLBanGiay_64130531Entities db = new QLBanGiay_64130531Entities();
        // GET: Thanhtoan_64130531
        public async Task<UserH> getUser()
        {
            try
            {
                // Lấy giá trị idUser từ cookie
                string idUser = Request.Cookies["idUser"]?.Value;

                // Nếu idUser không tồn tại, trả về null hoặc một đối tượng mặc định
                if (string.IsNullOrEmpty(idUser))
                {
                    return null;
                }

                // Tìm thông tin người dùng từ cơ sở dữ liệu
                var user = db.Users.FirstOrDefault(u => u.taikhoan == idUser); // Giả sử "Users" là bảng chứa thông tin người dùng

                // Nếu không tìm thấy người dùng, trả về null
                if (user == null)
                {
                    return null;
                }

                // Tạo và trả về đối tượng UserH
                var userH = new UserH
                {
                    Taikhoan = user.taikhoan,
                    Ten = user.ten,
                    sdt = user.sdt,
                    Email = user.email,
                    Diachi = user.diaChi,
                    diachinhanhang = user.diaChi // Giả sử có trường diachiNhanhang trong User
                };

                return userH;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<ActionResult> Index(string jsonData, string tongtien)
        {
            List<donmua> cthd = new List<donmua>();

            if (!string.IsNullOrEmpty(jsonData))
            {
                cthd = JsonConvert.DeserializeObject<List<donmua>>(jsonData);
            }

            var nguoidung = await getUser();

            // Trả về view với Model là giỏ hàng (cthd) và người dùng (nguoidung)
            ViewBag.nguoidung = nguoidung;
            ViewBag.tongTien = tongtien;

            return View(cthd); // Trả về Model giỏ hàng
        }

        [HttpPost]
        public ActionResult Thanhtoan(CheckoutRequest request)
        {
            try
            {
                // Kiểm tra người dùng
                var user = db.Users.FirstOrDefault(u => u.taikhoan == request.User.Taikhoan);
                if (user == null)
                {
                    return Json(new { success = false, message = "Người dùng không tồn tại." });
                }

                // Tạo mã hóa đơn
                var random = new Random();
                var randomString = GenerateRandomString(6); // Tạo chuỗi ngẫu nhiên 6 ký tự
                var userSuffix = request.User.Taikhoan.Substring(request.User.Taikhoan.Length - 2); // Lấy 2 ký tự cuối của mã người dùng
                var maHoadon = userSuffix + randomString; // Tạo mã hóa đơn với thời gian, mã người dùng và chuỗi ngẫu nhiên

                // Tạo đối tượng hóa đơn
                var hoadon = new Hoadon

                {
                    maHoadon = maHoadon,
                    diaChi = request.User.diachinhanhang,
                    ngayTao = DateTime.Now,
                    ngayGiao = null,
                    id_Trangthai = 1,
                    tongTien = request.TongTien,
                    taikhoan = request.User.Taikhoan
                };

                //// Kiểm tra chi tiết hóa đơn
                //var cthd = db.Chitiethoadon.ToList();
                //foreach (var item in request.Ctdm)
                //{
                //    var chitiet = cthd.FirstOrDefault(ct => ct.id_chitietHD == item.idCthd);
                //    if (chitiet != null)
                //    {
                //        chitiet.maHoadon = maHoadon; // Gán mã hóa đơn cho chi tiết hóa đơn
                //    }
                //    else
                //    {
                //        // Nếu không tìm thấy chi tiết hóa đơn, trả về thông báo lỗi
                //        return Json(new { success = false, message = "Không tìm thấy chi tiết hóa đơn với ID: " + item.idCthd });
                //    }
                //}

                // Cập nhật chi tiết hóa đơn
                foreach (var item in request.Ctdm) // `Ctdm` là danh sách chi tiết đơn hàng từ request
                {
                    var chitiet = db.Chitiethoadon.FirstOrDefault(ct => ct.id_chitietHD == item.idCthd);
                    if (chitiet != null)
                    {
                        chitiet.maHoadon = maHoadon; // Cập nhật mã hóa đơn cho chi tiết đơn hàng
                    }
                }

                // Thêm hóa đơn vào cơ sở dữ liệu và lưu thay đổi
                db.Hoadon.Add(hoadon);
                db.SaveChanges(); // Lưu vào cơ sở dữ liệu

                return Json(new { success = true, message = "Thanh toán thành công!", redirectUrl = Url.Action("History", "History_64130531") });
            }
            catch (Exception ex)
            {
                // Ghi chi tiết lỗi
                Console.WriteLine("Lỗi: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception: " + ex.InnerException.Message);
                }
                return Json(new { success = false, message = "Đã xảy ra lỗi: " + ex.Message });
            }
        }
        private string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[random.Next(chars.Length)])
                .ToArray());
        }
    }
}