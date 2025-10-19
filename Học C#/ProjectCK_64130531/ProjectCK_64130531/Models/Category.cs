using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCK_64130531.Models
{
    public class Category
    {
        public string IdChitietSP { get; set; }  // Mã chi tiết sản phẩm
        public string HinhAnh { get; set; }      // Hình ảnh của sản phẩm
        public int SoLuongTon { get; set; }      // Số lượng tồn kho
        public Nullable<decimal> Gia { get; set; }         // Giá của sản phẩm
        public int MaMau { get; set; }        // Mã màu của sản phẩm
        public string TenSanpham { get; set; }   // Tên sản phẩm
        public string TenNhan { get; set; }      // Tên nhãn hiệu
        public string TenMau { get; set; }       // Tên màu
        public string MoTa { get; set; }         // Mô tả sản phẩm (Dành cho trang chi tiết sản phẩm)
    }
}
