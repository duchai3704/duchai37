using System;
using System.Linq;
using System.Web.Mvc;
using ProjectCK_64130531.Models;

namespace ProjectCK_64130531.Controllers
{
    public class Blog_64130531Controller : Controller
    {
        // Sử dụng database context
        private QLBanGiay_64130531Entities db = new QLBanGiay_64130531Entities();

        public ActionResult Blog(string returnUrl = "")
        {
            // Kiểm tra thông tin người dùng trong Session
            var user = Session["UserSession"] as Users;

            if (user == null)
            {
                // Lưu lại URL trang Blog để sau khi đăng nhập xong sẽ chuyển hướng về trang Blog
                returnUrl = Url.Action("Blog", "Blog_64130531");
                TempData["EROR"] = "Bạn phải đăng nhập trước.";
                return RedirectToAction("Login", "Account_64130531", new { returnUrl });
            }

            // Tìm kiếm người dùng trong cơ sở dữ liệu để đảm bảo hợp lệ
            var dbUser = db.Users.FirstOrDefault(u => u.taikhoan == user.taikhoan && u.matkhau == user.matkhau);

            if (dbUser == null)
            {
                TempData["EROR"] = "Tài khoản không tồn tại trong hệ thống.";
                Session["UserSession"] = null; // Xóa session
                return RedirectToAction("Login", "Account_64130531");
            }

            // Nếu đã đăng nhập và thông tin hợp lệ, chuyển đến trang Blog
            return View();
        }
    }
}
