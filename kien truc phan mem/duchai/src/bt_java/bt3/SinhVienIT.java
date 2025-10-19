package bt_java.bt3;

public class SinhVienIT extends SinhVienNTU {
    private float diemJava, diemCSS, diemHTML;

    public SinhVienIT(String hoTen, String nganh, float diemJava, float diemHTML, float diemCSS) {
        super(hoTen, nganh);
        this.diemJava = diemJava;
        this.diemHTML = diemHTML;
        this.diemCSS = diemCSS;
    }

    @Override
    public float getDiemTB(){
        return (diemJava*2 + diemCSS + diemHTML)/4;
    }


    @Override
    public  void inThongTin(){
        super.inThongTin();
        System.out.println("Điểm: ");
        System.out.println("JAVA: " + diemJava + ", CSS: " + diemCSS + ", HTML: " + diemHTML);
        System.out.println("Điểm TB: " + this.getDiemTB());
        System.out.println("Học lực: " + this.getHocLuc());
    }
}
