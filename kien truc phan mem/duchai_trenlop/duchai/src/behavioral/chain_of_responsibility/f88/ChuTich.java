package behavioral.chain_of_responsibility.f88;

public class ChuTich extends NhanVienF88{
    public ChuTich(String chucVu, String ten, int hanMucDuyetVay) {
        super(chucVu, ten, hanMucDuyetVay);
    }

    public void duyetChoVay(int khoanVay) {
        if (khoanVay <= this.hanMucDuyetVay) {
            System.out.println(this.chucVu + " " + this.ten + "duyệt khoản vay" + khoanVay);
        } else {
            System.out.println("Ra ngân hàng vay");
        }

    }

    public NhanVienF88 capTren(NhanVienF88 capTren) {
        return null;
    }
}
