package behavioral.chain_of_responsibility.f88;

public abstract class NhanVienF88 {
    String chucVu;
    String ten;
    int hanMucDuyetVay;

    public NhanVienF88(String chucVu, String ten, int hanMucDuyetVay) {
        this.chucVu = chucVu;
        this.ten = ten;
        this.hanMucDuyetVay = hanMucDuyetVay;
    }

    public abstract void duyetChoVay(int var1);

    public abstract NhanVienF88 capTren(NhanVienF88 var1);
}
