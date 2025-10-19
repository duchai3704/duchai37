package bt_java.bt3;

public class SinhVienBiz extends SinhVienNTU{
    private float diemMarketing, diemSales;

    public SinhVienBiz(String hoTen, String nganh, float diemMarketing, float diemSales) {
        super(hoTen, nganh);
        this.diemMarketing = diemMarketing;
        this.diemSales = diemSales;
    }

    @Override
    public float getDiemTB() {
        return (diemMarketing*2 + diemSales)/3;
    }

    @Override
    public void inThongTin() {
        super.inThongTin();
        System.out.println("Điểm: ");
        System.out.println("Marketing: " + diemMarketing + ", Sales: " + diemSales);
        System.out.println("Điểm TB: " + this.getDiemTB());
        System.out.println("Học lực: " + this.getHocLuc());
    }
}
