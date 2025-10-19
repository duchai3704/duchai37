package behavioral.chain_of_responsibility.f88;

public class MainF88 {
    public static void main(String[] args) {
        NhanVienF88 nvhs = new NhanVienChoVayF88("Nhân viên nhận hồ sơ", "Đức Hải", 5000000);
        NhanVienF88 thuky = new NhanVienChoVayF88("Thư ký", "kiệt", 10000000);
        NhanVienF88 phophong = new NhanVienChoVayF88("Phó phòng", "Bảo", 20000000);
        NhanVienF88 truongphong = new NhanVienChoVayF88("Trưởng phòng", "Trực", 50000000);
        NhanVienF88 giamdoc = new NhanVienChoVayF88("Giám đốc", "Tuấn", 80000000);
        NhanVienF88 chutich = new NhanVienChoVayF88("Chủ tịch", "Phi", 100000000);
        nvhs.capTren(thuky).capTren(phophong).capTren(truongphong).capTren(giamdoc).capTren(chutich);
        nvhs.duyetChoVay(75000000);
    }
}
