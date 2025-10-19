package behavioral.chain_of_responsibility.atm;

public abstract class MenhGiaATM {
    protected MenhGiaATM keTiep;
    protected int giaTri;

    public MenhGiaATM(int giaTri) {
        this.giaTri = giaTri;
    }

    public abstract MenhGiaATM thietLapKeTiep(MenhGiaATM k);
    public abstract void rutTien(int soTien);
}
