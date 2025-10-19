package state.atm;

public class ATM {
    int soTien;
    ATM_State state;


    public ATM(){
        soTien = 0;
        state = new HetTien();
    }

    public void setState(ATM_State state){
        this.state = state;
    }

    public void napTien(int soTien){
        state.xuLyNapTien(this, soTien);
    }

    public void rutTien(){
        state.xuLyRutTien(this);
    }

    public int getSoTien() {
        return soTien;
    }

    public void setSoTien(int soTien) {
        this.soTien = soTien;
    }

    public ATM_State getState() {
        return state;
    }
}
