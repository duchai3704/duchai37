package state.atm;

public class MainATMState {
    public static void main(String[] args) {
        ATM atm = new ATM();
        atm.napTien(7_000_000);
        atm.rutTien();
        atm.rutTien();
        atm.rutTien();
    }
}
