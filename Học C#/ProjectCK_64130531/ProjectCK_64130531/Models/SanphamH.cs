using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ProjectCK_64130531.Models
{
    public class SanphamH
    {
        public string TenSanPham { get; set; }
        public string MoTa { get; set; }
        public List<ChitietsanphamH> ChiTietSanPhamList { get; set; }
        public int PhanTramKhuyenMai { get; set; }
    }
}