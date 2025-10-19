package bt_java.bt3;

public class BT3Main {
    public static void main(String[] args) {
        SinhVienNTU svNTU = new SinhVienIT("HAI", "CNTT", 9.0F, 8.0F, 8.0F);
        svNTU.inThongTin();

        SinhVienNTU svIT = new SinhVienIT("KIET", "IT", 9.0F, 8.0F, 7.0F);
        svIT.inThongTin();

        SinhVienNTU svBiz = new SinhVienBiz("BAO", "BIZ", 7.0F, 8.0F);
        svBiz.inThongTin();
    }
}
