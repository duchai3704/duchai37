package bt_java.bt3;

public abstract class SinhVienNTU {
    private String hoTen;
    private String nganh;
    public SinhVienNTU(String hoTen, String nganh){
        this.hoTen = hoTen;
        this.nganh = nganh;
    }

   public abstract float getDiemTB();

    public String getHocLuc(){
        float diemTB = getDiemTB();
        if(diemTB >= 8)
            return "Giỏi";
        else if(diemTB >= 6.5)
            return "Khá";
        else if(diemTB >= 5)
            return "Trung bình";
        else
            return "Yếu";
    }


    public void inThongTin(){
        System.out.println("Họ tên: " + this.hoTen);
        System.out.println(("Ngành: " + this.nganh));
    }


    public String getHoTen() {
        return hoTen;
    }

    public void setHoTen(String hoTen) {
        this.hoTen = hoTen;
    }

    public String getNganh() {
        return nganh;
    }

    public void setNganh(String nganh) {
        this.nganh = nganh;
    }
}
