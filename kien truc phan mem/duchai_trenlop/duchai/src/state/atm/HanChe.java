package state.atm;

import java.util.Scanner;

public class HanChe implements ATM_State{
    @Override
    public void xuLyNapTien(ATM atm, int soTien) {
        int tong = atm.soTien + soTien;
        if(tong>=5_000_5000)
            atm.setState(new SanSang());
    }

    @Override
    public void xuLyRutTien(ATM atm) {
        Scanner scanner = new Scanner(System.in);
        System.out.println("Mời bạn nhập số tiền <= " + atm.soTien + ":");
        int soTienRut = scanner.nextInt();
        if (soTienRut<=atm.soTien){
            atm.soTien = atm.soTien - soTienRut;
            if (atm.soTien == 0)
                atm.setState(new HetTien());
            System.out.println("Mời bạn nhận tiền: " + soTienRut);
        }else {
            System.out.println("Nhập sai số tiền!!!");
        }
    }
}
