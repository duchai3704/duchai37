package bt_java.a6singleton;

public class MainA6 {
    public static void main(String[] args) {
        DataAccess d1 = DataAccess.getInstance();
        DataAccess d2 = DataAccess.getInstance();
        DataAccess d3 = DataAccess.getInstance();
        d1.them(new SanPham("1", "Laptop"));
        d2.them(new SanPham("2", "Điện thoại"));
        d3.them(new SanPham("3", "Máy tính bảng"));
        DataAccess.getInstance().inKQ();
    }
}
