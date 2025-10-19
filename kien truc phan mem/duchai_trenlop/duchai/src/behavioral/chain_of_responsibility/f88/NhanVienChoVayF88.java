package behavioral.chain_of_responsibility.f88;

public class NhanVienChoVayF88 extends NhanVienF88 {
    NhanVienF88 capTren;

    public NhanVienChoVayF88(String chucVu, String ten, int hanMucDuyetVay) {
        super(chucVu, ten, hanMucDuyetVay);
    }

    public void duyetChoVay(int khoanVay) {
        if (khoanVay <= this.hanMucDuyetVay) {
            System.out.println(this.chucVu + " " + this.ten + " duyệt khoản vay " + khoanVay);
        } else {
            this.capTren.duyetChoVay(khoanVay);
        }

    }

    public NhanVienF88 capTren(NhanVienF88 capTren) {
        this.capTren = capTren;
        return this.capTren;
    }
}