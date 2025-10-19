using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectCK_64130531.Models;
using System.Web.UI;
using Newtonsoft.Json;
using System.Net.Http;

using System.Data.Entity;
using System.Threading.Tasks;

namespace ProjectCK_64130531.Controllers
{
    public class Category_64130531Controller : Controller
    {
        QLBanGiay_64130531Entities db = new QLBanGiay_64130531Entities();
        // GET: Category_64130531
        public async Task<ActionResult> Category_64130531(int page = 1)
        {
            // Lấy danh sách danh mục, thương hiệu, màu sắc và chi tiết sản phẩm từ các phương thức async
            List<DanhMucDH> danhMucList = await GetDanhmuc();
            List<BrandDH> brandList = await GetBrand();
            List<ColorDH> colorList = await GetColor();
            List<ChiTietSanPhamDH> sanphamList = await GetChiTietSanPham();

            // Số sản phẩm hiển thị mỗi trang cho danh sách 6 sản phẩm
            int pageSize1 = 6;
            int skip1 = (page - 1) * pageSize1;
            List<ChiTietSanPhamDH> paginatedSanphamList1 = sanphamList.Skip(skip1).Take(pageSize1).ToList();

            // Số sản phẩm hiển thị cho danh sách 4 sản phẩm
            int pageSize2 = 9;
            List<ChiTietSanPhamDH> paginatedSanphamList2 = sanphamList.Take(pageSize2).ToList();

            // Tính tổng số trang cho danh sách 6 sản phẩm
            int totalPages = (int)Math.Ceiling((double)sanphamList.Count / pageSize1);

            // Gán vào ViewModel
            CategoryViewModel viewModel = new CategoryViewModel
            {
                DanhMucs = danhMucList,
                Brands = brandList,
                Colors = colorList,
                SanPhams6 = paginatedSanphamList1,  // Danh sách 6 sản phẩm
                SanPhams9 = paginatedSanphamList2,  // Danh sách 4 sản phẩm
                CurrentPage = page,
                TotalPages = totalPages
            };

            // Truyền ViewModel vào View
            return View(viewModel);
        }
        [HttpGet]
        public async Task<List<DanhMucDH>> GetDanhmuc()
        {
            try
            {
                // Truy vấn danh mục từ cơ sở dữ liệu
                var result = await (from sp in db.Danhmuc
                             select new DanhMucDH
                             {
                                 MaDanhMuc = sp.maDanhmuc,  // Gán giá trị vào thuộc tính MaDanhmuc của DanhMucDH
                                 TenDanhMuc = sp.tenDanhmuc, // Gán giá trị vào thuộc tính TenDanhmuc của DanhMucDH
                             }).ToListAsync();

                // Kiểm tra và trả về kết quả
                return result;  // Trả về danh sách danh mục

            }
            catch (Exception ex)
            {
                return new List<DanhMucDH>();  // Trả về danh sách rỗng nếu có lỗi
            }
        }
        [HttpGet]
        public async Task<List<BrandDH>> GetBrand()
        {
            try
            {
                var result = await (from sp in db.Nhanhieu
                             select new BrandDH
                             {
                                 MaNhan = sp.maNhan,
                                 TenNhan = sp.tenNhan,
                             }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                return new List<BrandDH>();
            }
        }
        [HttpGet]
        public async Task<List<ColorDH>> GetColor()
        {
            try
            {
                var result = await (from sp in db.Color
                             select new ColorDH
                             {
                                 MaMau = sp.maMau,
                                 TenMau = sp.tenMau,
                             }).ToListAsync(); ;
                return result;
            }
            catch (Exception ex)
            {
                return new List<ColorDH>();
            }
        }
        [HttpGet]
        public async Task<List<ChiTietSanPhamDH>> GetChiTietSanPham()
        {
            try
            {
                // Truy vấn chi tiết sản phẩm và thông tin sản phẩm từ bảng Sanpham
                var result = await (from cts in db.Chitietsanpham
                             join sp in db.Sanpham on cts.maSanpham equals sp.maSanpham
                             select new ChiTietSanPhamDH
                             {
                                 ChitietId = cts.id_chitietSP,
                                 HinhAnh = cts.hinhAnh,
                                 SoLuongCon = cts.soLuongTon,
                                 Gia = cts.gia,
                                 mamau = cts.maMau,
                                 masize = cts.maSize,
                                 TenSanPham = sp.tenSanpham,
                                 MauSac = cts.tenMau,
                                 MaSanPham = sp.maSanpham
                             }).ToListAsync(); ;

                // Trả về danh sách kết quả
                return result;
            }
            catch (Exception ex)
            {
                // Log lỗi nếu có sự cố
                Console.WriteLine(ex.Message); // Log lỗi vào console (hoặc ghi vào file log nếu cần)
                return new List<ChiTietSanPhamDH>(); // Trả về danh sách rỗng nếu có lỗi
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetProductsByCategory(int maDanhMuc)
        {
            try
            {
                var result = await (from sp in db.Sanpham
                                    join cts in db.Chitietsanpham on sp.maSanpham equals cts.maSanpham
                                    where sp.maDanhmuc == maDanhMuc
                                    select new ChiTietSanPhamDH
                                    {
                                        ChitietId = cts.id_chitietSP,
                                        HinhAnh = cts.hinhAnh,
                                        SoLuongCon = cts.soLuongTon,
                                        Gia = cts.gia,
                                        mamau = cts.maMau,
                                        masize = cts.maSize,
                                        TenSanPham = sp.tenSanpham,
                                        MauSac = cts.tenMau,
                                        MaSanPham = sp.maSanpham // Assuming you want the `maSanpham` from `Sanpham`
                                    }).ToListAsync();

                return Json(result, JsonRequestBehavior.AllowGet); // Return JSON response
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); // Return error as JSON if something goes wrong
            }
        }


        [HttpGet]
        public async Task<ActionResult> GetFilterByBrand(int maNhan)
        {
            try
            {
                var result = await (from nh in db.Nhanhieu
                                    join sp in db.Sanpham on nh.maNhan equals sp.maNhan
                                    join cts in db.Chitietsanpham on sp.maSanpham equals cts.maSanpham

                                    where sp.maNhan == maNhan // Lọc theo MaDanhMuc = 1
                                    select new ChiTietSanPhamDH
                                    {
                                        ChitietId = cts.id_chitietSP,
                                        HinhAnh = cts.hinhAnh,
                                        SoLuongCon = cts.soLuongTon,
                                        Gia = cts.gia,
                                        mamau = cts.maMau,
                                        masize = cts.maSize,
                                        TenSanPham = sp.tenSanpham,
                                        MauSac = cts.tenMau,
                                        MaSanPham = sp.maSanpham // Assuming you want the `maSanpham` from `Sanpham`
                                    }).ToListAsync();

                return Json(result, JsonRequestBehavior.AllowGet); // Return JSON response
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); // Return error as JSON if something goes wrong
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetFilterByColor(int maMau)
        {
            try
            {
                var result = await (from cts in db.Chitietsanpham
                                    join sp in db.Sanpham on cts.maSanpham equals sp.maSanpham
                                    where cts.maMau == maMau // Lọc theo MaDanhMuc = 1
                                    select new ChiTietSanPhamDH
                                    {
                                        ChitietId = cts.id_chitietSP,
                                        HinhAnh = cts.hinhAnh,
                                        SoLuongCon = cts.soLuongTon,
                                        Gia = cts.gia,
                                        mamau = cts.maMau,
                                        masize = cts.maSize,
                                        TenSanPham = sp.tenSanpham,
                                        MauSac = cts.tenMau,
                                        MaSanPham = sp.maSanpham // Assuming you want the `maSanpham` from `Sanpham`
                                    }).ToListAsync();

                return Json(result, JsonRequestBehavior.AllowGet); // Return JSON response
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); // Return error as JSON if something goes wrong
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetByPriceRange(int gia1, int gia2)
        {
            try
            {
                // Truy vấn sản phẩm và chi tiết sản phẩm trong phạm vi giá
                var result = await (from cts in db.Chitietsanpham
                                    join sp in db.Sanpham on cts.maSanpham equals sp.maSanpham
                                    where cts.gia >= gia1 && cts.gia <= gia2 // Điều kiện lọc theo giá
                                    select new ChiTietSanPhamDH
                                    {
                                        ChitietId = cts.id_chitietSP,
                                        HinhAnh = cts.hinhAnh,
                                        SoLuongCon = cts.soLuongTon,
                                        Gia = cts.gia,
                                        mamau = cts.maMau,
                                        masize = cts.maSize,
                                        TenSanPham = sp.tenSanpham,
                                        MauSac = cts.tenMau,
                                        MaSanPham = sp.maSanpham // Assuming you want the `maSanpham` from `Sanpham`
                                    }).ToListAsync();

                return Json(result, JsonRequestBehavior.AllowGet); // Return JSON response
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet); // Return error as JSON if something goes wrong
            }
        }
    }
}