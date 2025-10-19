using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectCK_64130531.Models;

namespace ProjectCK_64130531.Controllers
{
    public class Account_64130531Controller : Controller
    {
        // Sử dụng database context
        private QLBanGiay_64130531Entities db = new QLBanGiay_64130531Entities();

        [HttpPost]
        public ActionResult GetUser(string Taikhoan, string Matkhau, string returnUrl = "")
        {
            // Kiểm tra input
            if (string.IsNullOrEmpty(Taikhoan) || string.IsNullOrEmpty(Matkhau))
            {
                TempData["EROR"] = "Tài khoản hoặc mật khẩu không được để trống.";
                return RedirectToAction("Login");
            }

            try
            {
                // Tìm kiếm tài khoản trong database
                var user = db.Users
                             .FirstOrDefault(u => u.taikhoan == Taikhoan && u.matkhau == Matkhau);

                if (user != null)
                {
                    // Nếu tìm thấy, lưu user vào Session
                    Session["UserSession"] = user;

                    // Tạo cookie lưu tài khoản
                    HttpCookie cookie = new HttpCookie("idUser", user.taikhoan)
                    {
                        Expires = DateTime.Now.AddDays(1) // Thời hạn 1 ngày
                    };
                    Response.Cookies.Add(cookie);

                    // Điều hướng đến trang được yêu cầu hoặc mặc định
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Information");
                    }
                }
                else
                {
                    TempData["EROR"] = "Sai tài khoản hoặc mật khẩu.";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                TempData["EROR"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction("Login");
            }
        }
    

        // Thêm chức năng đăng xuất
        public ActionResult Logout()
        {
            Session["UserSession"] = null;

            if (Request.Cookies["idUser"] != null)
            {
                var cookie = new HttpCookie("idUser")
                {
                    Expires = DateTime.Now.AddDays(-1) // Xóa cookie bằng cách đặt ngày hết hạn về quá khứ
                };
                Response.Cookies.Add(cookie);
            }

            return RedirectToAction("Login");
        }


        public ActionResult Login(string returnUrl = "")
        {
            ViewBag.ReturnUrl = returnUrl;  // Lưu returnUrl vào ViewBag để sử dụng trong view Login
            return View();
        }

        public ActionResult Information()
        {
            // Kiểm tra user trong Session
            var user = Session["UserSession"] as Users;
            if (user == null)
            {
                TempData["EROR"] = "Bạn phải đăng nhập trước.";
                return RedirectToAction("Login", "Account_64130531");
            }

            return View(user);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Register(string Taikhoan, string Matkhau, string Ten, string Sdt, string Email, string DiaChi)
        {
            if (string.IsNullOrEmpty(Taikhoan) || string.IsNullOrEmpty(Matkhau) || string.IsNullOrEmpty(Ten) || string.IsNullOrEmpty(Sdt) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(DiaChi))
            {
                TempData["ERROR"] = "Vui lòng điền đầy đủ thông tin.";
                return RedirectToAction("Register");
            }

            try
            {
                // Kiểm tra tài khoản đã tồn tại
                var existingUser = db.Users.FirstOrDefault(u => u.taikhoan == Taikhoan);
                if (existingUser != null)
                {
                    TempData["ERROR"] = "Tài khoản đã tồn tại.";
                    return RedirectToAction("Register");
                }

                // Tạo người dùng mới và lưu vào cơ sở dữ liệu
                var newUser = new Users
                {
                    taikhoan = Taikhoan,
                    matkhau = Matkhau,
                    ten = Ten,
                    sdt = Sdt,
                    email = Email,
                    diaChi = DiaChi
                };

                var GioHang = new Hoadon
                {
                    maHoadon = newUser.taikhoan.Substring(newUser.taikhoan.Length - 1) + "0", // Cắt 1 ký tự cuối và kết hợp với 1 số 0
                    diaChi = DiaChi,
                    ngayTao = DateTime.Now,  // Ví dụ thêm ngày tạo
                    ngayGiao = null,         // Ví dụ thêm ngày giao
                    id_Trangthai = 0,        // Ví dụ thêm trạng thái
                    tongTien = 0,            // Ví dụ thêm tổng tiền
                    taikhoan = newUser.taikhoan     // Thêm tài khoản
                };

                db.Users.Add(newUser);
                db.Hoadon.Add(GioHang);
                db.SaveChanges();

                TempData["SUCCESS"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = "Lỗi khi đăng ký: " + ex.Message;
                return RedirectToAction("Register");
            }
        }

        public class RegisterUser
        {
            public string Taikhoan { get; set; }
            public string Matkhau { get; set; }
            public string Ten { get; set; }
            public string Sdt { get; set; }
            public string Email { get; set; }
            public string DiaChi { get; set; }
        }
    }
}