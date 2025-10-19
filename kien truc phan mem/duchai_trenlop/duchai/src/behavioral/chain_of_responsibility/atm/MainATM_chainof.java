package behavioral.chain_of_responsibility.atm;

public class MainATM_chainof {
    public static void main(String[] args) {
        MenhGiaATM m1 = new MenhGiaThapNhat(1);
        MenhGiaATM m2 = new MenhGia(2);
        MenhGiaATM m5 = new MenhGia(5);
        MenhGiaATM m10 = new MenhGia(10);
        MenhGiaATM m20 = new MenhGia(20);
        MenhGiaATM m50 = new MenhGia(50);
        MenhGiaATM m100 = new MenhGia(100);
        MenhGiaATM m200 = new MenhGia(200);
        MenhGiaATM m500 = new MenhGia(500);

        m500.thietLapKeTiep(m200)
                .thietLapKeTiep(m100)
                .thietLapKeTiep(m50)
                .thietLapKeTiep(m20)
                .thietLapKeTiep(m10)
                .thietLapKeTiep(m5)
                .thietLapKeTiep(m2)
                .thietLapKeTiep(m1);

        m500.rutTien(728);
    }
}
