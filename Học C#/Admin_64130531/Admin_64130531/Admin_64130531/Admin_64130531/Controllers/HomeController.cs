using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Admin_64130531.Models;
using Newtonsoft.Json;

namespace Admin_64130531.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly Admin_64130531.Models.QLBanGiay_64130531Entities _db;
     
        public HomeController()
        {
            _db = new Admin_64130531.Models.QLBanGiay_64130531Entities();
        }

        [HttpGet]
        public async Task<List<BestSellingProduct>> GetTop10BestSellingProductsAsync()
        {
            try
            {
                var top10BestSellingProducts = await (
                    from chitiethoadon in _db.Chitiethoadon
                    join chitietsanpham in _db.Chitietsanpham
                        on chitiethoadon.id_chitietSP equals chitietsanpham.id_chitietSP
                    join sanpham in _db.Sanpham
                        on chitietsanpham.maSanpham equals sanpham.maSanpham
                    where chitiethoadon.soLuong.HasValue
                    group chitiethoadon by new
                    {
                        chitietsanpham.id_chitietSP,
                        sanpham.tenSanpham,
                        chitietsanpham.gia
                    }
                    into grouped
                    orderby grouped.Sum(x => x.soLuong ?? 0) descending
                    select new BestSellingProduct
                    {
                        ProductId = grouped.Key.id_chitietSP,
                        ProductName = grouped.Key.tenSanpham,
                        ProductPrice = grouped.Key.gia,
                        TotalQuantitySold = grouped.Sum(x => x.soLuong ?? 0)
                    }
                ).Take(10).ToListAsync();

                // Trả về JSON với JsonRequestBehavior.AllowGet
                return top10BestSellingProducts;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;

            }
        }


        private async Task<Thongke> GetRevenueAsync(Func<Admin_64130531.Models.QLBanGiay_64130531Entities, Task<Thongke>> queryFunc)
        {
            return await queryFunc(_db);
        }

        public async Task<Thongke> GetRevenueForToday()
        {
            return await GetRevenueAsync(async context =>
            {
                var today = DateTime.Today;
                var yesterday = today.AddDays(-1);

                // First, get the data from the database without filtering by date
                var allRevenue = await context.Hoadon
                    .Where(h => h.ngayTao.HasValue) // Ensure we have valid dates
                    .ToListAsync();

                // Filter in memory (after retrieval) based on Date components
                var todayRevenue = allRevenue
                    .Where(h => h.ngayTao.Value.Year == today.Year
                                && h.ngayTao.Value.Month == today.Month
                                && h.ngayTao.Value.Day == today.Day) // Compare individual date parts
                    .GroupBy(h => new { h.ngayTao.Value.Year, h.ngayTao.Value.Month, h.ngayTao.Value.Day })
                    .Select(g => new Thongke
                    {
                        Label = today.ToString("yyyy/MM/dd"),
                        Sotien = g.Sum(h => h.tongTien ?? 0)
                    })
                    .FirstOrDefault() ?? new Thongke { Label = today.ToString("yyyy/MM/dd"), Sotien = 0 };

                var yesterdayRevenue = allRevenue
                    .Where(h => h.ngayTao.Value.Year == yesterday.Year
                                && h.ngayTao.Value.Month == yesterday.Month
                                && h.ngayTao.Value.Day == yesterday.Day) // Compare individual date parts
                    .Sum(h => h.tongTien ?? 0);

                todayRevenue.thaydoi = yesterdayRevenue == 0
                    ? 0
                    : ((todayRevenue.Sotien - yesterdayRevenue) / yesterdayRevenue) * 100;

                return todayRevenue;
            });
        }

        public async Task<Thongke> GetRevenueForMonth()
        {
            return await GetRevenueAsync(async context =>
            {
                var today = DateTime.Today;
                var previousMonth = today.AddMonths(-1);

                // First, get the data from the database without filtering by month or year
                var allRevenue = await context.Hoadon
                    .Where(h => h.ngayTao.HasValue) // Ensure we have valid dates
                    .ToListAsync();

                // Filter in memory (after retrieval) based on month and year components
                var currentMonthRevenue = allRevenue
                    .Where(h => h.ngayTao.Value.Year == today.Year && h.ngayTao.Value.Month == today.Month) // Filter by current month and year
                    .GroupBy(h => $"Tháng {today.Month}/{today.Year}")
                    .Select(g => new Thongke
                    {
                        Label = g.Key,
                        Sotien = g.Sum(h => h.tongTien ?? 0)
                    })
                    .FirstOrDefault() ?? new Thongke { Label = $"Tháng {today.Month}/{today.Year}", Sotien = 0 };

                // Filter the previous month's revenue in memory
                var previousMonthRevenue = allRevenue
                    .Where(h => h.ngayTao.Value.Year == previousMonth.Year && h.ngayTao.Value.Month == previousMonth.Month) // Filter by previous month and year
                    .Sum(h => h.tongTien ?? 0);

                currentMonthRevenue.thaydoi = previousMonthRevenue == 0
                    ? 0
                    : ((currentMonthRevenue.Sotien - previousMonthRevenue) / previousMonthRevenue) * 100;

                return currentMonthRevenue;
            });
        }

        public async Task<Thongke> GetRevenueForYear()
        {
            return await GetRevenueAsync(async context =>
            {
                var today = DateTime.Today;
                var previousYear = today.Year - 1;

                // First, get the data from the database without filtering by year
                var allRevenue = await context.Hoadon
                    .Where(h => h.ngayTao.HasValue) // Ensure we have valid dates
                    .ToListAsync();

                // Filter in memory (after retrieval) based on year
                var currentYearRevenue = allRevenue
                    .Where(h => h.ngayTao.Value.Year == today.Year) // Filter by current year
                    .GroupBy(h => $"Năm {today.Year}")
                    .Select(g => new Thongke
                    {
                        Label = g.Key,
                        Sotien = g.Sum(h => h.tongTien ?? 0)
                    })
                    .FirstOrDefault() ?? new Thongke { Label = $"Năm {today.Year}", Sotien = 0 };

                // Filter the previous year's revenue in memory
                var previousYearRevenue = allRevenue
                    .Where(h => h.ngayTao.Value.Year == previousYear) // Filter by previous year
                    .Sum(h => h.tongTien ?? 0);

                currentYearRevenue.thaydoi = previousYearRevenue == 0
                    ? 0
                    : ((currentYearRevenue.Sotien - previousYearRevenue) / previousYearRevenue) * 100;

                return currentYearRevenue;
            });
        }

        public async Task<Thongke> GetRevenueForYear(int year1)
        {
            return await GetRevenueAsync(async (context) =>
            {
                var currentYearRevenue = await context.Hoadon
                                                      .Where(h => h.ngayTao.HasValue && h.ngayTao.Value.Year == year1)  // Lọc dữ liệu theo năm
                                                      .GroupBy(h => h.ngayTao.Value.Year)  // Nhóm theo năm thực tế
                                                      .Select(g => new Thongke
                                                      {
                                                          Label = $"Năm {year1}",
                                                          Sotien = g.Sum(h => h.tongTien ?? 0)
                                                      })
                                                      .FirstOrDefaultAsync()
                                                      ?? new Thongke { Label = $"Năm {year1}", Sotien = 0 };

                return currentYearRevenue;
            });
        }

        private async Task<decimal> GetRevenueForSpecificMonth(int year, int month)
        {
            return await _db.Hoadon
                            .Where(h => h.ngayTao.HasValue && h.ngayTao.Value.Year == year && h.ngayTao.Value.Month == month)
                            .SumAsync(h => (decimal?)h.tongTien) ?? 0;
        }

        private async Task<decimal> GetRevenueForSpecificDay(int year, int month, int day)
        {
            return await _db.Hoadon
                            .Where(h => h.ngayTao.HasValue &&
                                        h.ngayTao.Value.Year == year &&
                                        h.ngayTao.Value.Month == month &&
                                        h.ngayTao.Value.Day == day)
                            .SumAsync(h => (decimal?)h.tongTien) ?? 0;
        }

        [HttpGet]
        public async Task<List<Thongke>> ThongkeDoanhthu(string type, int? year, int month)
        {
            try
            {
                var result = new List<Thongke>();

                if (type == "year" && year.HasValue)
                {
                    result = await _db.Hoadon
                        .Where(h => h.ngayTao.HasValue && h.ngayTao.Value.Year == year)
                        .GroupBy(h => h.ngayTao.Value.Month)
                        .Select(g => new Thongke
                        {
                            Label = "Tháng " + g.Key,
                            Sotien = g.Sum(h => h.tongTien) ?? 0
                        })
                        .ToListAsync();
                }
                else if (type == "year" && !year.HasValue)
                {
                    // Nếu year là null, trả về thống kê tất cả các năm
                    result = await _db.Hoadon
                        .Where(h => h.ngayTao.HasValue)
                        .GroupBy(h => h.ngayTao.Value.Year)
                        .Select(g => new Thongke
                        {
                            Label = "Năm " + g.Key,
                            Sotien = g.Sum(h => h.tongTien) ?? 0
                        })
                        .ToListAsync();
                }
                else if (type == "month")
                {
                    result = await _db.Hoadon
                        .Where(h => h.ngayTao.HasValue && h.ngayTao.Value.Year == year && h.ngayTao.Value.Month == month)
                        .GroupBy(h => h.ngayTao.Value.Day)
                        .Select(g => new Thongke
                        {
                            Label = "Ngày " + g.Key,
                            Sotien = g.Sum(h => h.tongTien) ?? 0
                        })
                        .ToListAsync();
                }
                else if (type == "day")
                {
                    result = await _db.Hoadon
                        .Where(h => h.ngayTao.HasValue && h.ngayTao.Value.Year == year && h.ngayTao.Value.Month == month)
                        .Select(h => new Thongke
                        {
                            Label = h.ngayTao.Value.ToString("dd/MM/yyyy"),
                            Sotien = h.tongTien ?? 0
                        })
                        .ToListAsync();
                }

                return result; // Trả về danh sách thống kê trực tiếp
            }
            catch (Exception ex)
            {
                // Ghi log lỗi chi tiết
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");

                // Trả về danh sách trống hoặc có thể tùy chỉnh
                return new List<Thongke>();
            }
        }






        public async Task<ActionResult> Index()
        {
            try
            {
                var bestSellingProducts = await GetTop10BestSellingProductsAsync();
                ViewBag.BestSellingProducts = bestSellingProducts;
                var thongkeResult = await ThongkeDoanhthu("year", null, 0);

                if (thongkeResult != null)
                {
                    // Chuyển đổi danh sách Thongke thành danh sách các đối tượng cần truyền cho View
                    var thongkeall = thongkeResult;

                    // Truyền thông tin về ViewBag
                    ViewBag.tkall = thongkeall.Select(item => new
                    {
                        Label = item.Label,  // Tên các tháng (January, February...)
                        Sotien = item.Sotien
                    }).ToList();
                }


                // Lấy doanh số theo năm
                var thongkenam = await ThongkeDoanhthu("year", 2025, 0);

                // Nếu ThongkeDoanhthu trả về JsonResult, chuyển đổi dữ liệu sang List<Thongke>
                var thongkeList = thongkenam;

                // Chọn các thông tin cần thiết và truyền vào ViewBag
                ViewBag.tknam = thongkeList.Select(item => new
                {
                    Label = item.Label,  // Tên các tháng (Tháng 1, Tháng 2...)
                    Sotien = item.Sotien // Số tiền tương ứng
                }).ToList();

                var todayRevenue = await GetRevenueForToday();
                ViewBag.TodayRevenue = todayRevenue;

                var monthlyRevenue = await GetRevenueForMonth();
                ViewBag.MonthlyRevenue = monthlyRevenue;

                var yearlyRevenue = await GetRevenueForYear();
                ViewBag.YearlyRevenue = yearlyRevenue;

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View("Error");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
