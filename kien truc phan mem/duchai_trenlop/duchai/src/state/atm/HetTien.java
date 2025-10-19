package state.atm;

public class HetTien implements ATM_State{
    @Override
    public void xuLyNapTien(ATM atm, int soTien) {
        int tong = soTien + atm.soTien;
        atm.setSoTien(tong);
        if(tong>=5_000_000)
            atm.setState(new SanSang());
        else
            if(tong > 0)
                atm.setState(new HanChe());
    }

    @Override
    public void xuLyRutTien(ATM atm) {
        System.out.println("Xin lỗi, máy ATM đã hết tiền!!!");
    }
}
