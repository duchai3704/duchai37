using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTTCS_64130531.Models
{
    public class CategoryViewModel
    {
        public List<DanhMucDH> DanhMucs { get; set; }
        public List<BrandDH> Brands { get; set; }
        public List<ColorDH> Colors { get; set; }
        public List<ChiTietSanPhamDH> SanPhams9 { get; set; }
        public List<ChiTietSanPhamDH> SanPhams6 { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}