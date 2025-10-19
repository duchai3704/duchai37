package behavioral.chain_of_responsibility.atm;

public class MenhGia extends MenhGiaATM{
    public MenhGia(int giaTri) {
        super(giaTri);
    }

    @Override
    public MenhGiaATM thietLapKeTiep(MenhGiaATM k) {
        keTiep = k;
        return k;
    }

    @Override
    public void rutTien(int soTien) {
        int soTo = soTien/giaTri;
        int soDu = soTien % giaTri;
        if (soTo>0){
            System.out.println(soTo + " tờ mệnh giá " + giaTri);
        }
        if (soDu>0)
            keTiep.rutTien(soDu);
    }
}
