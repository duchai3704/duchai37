package state.atm;

import java.util.Scanner;

public class SanSang implements ATM_State{
    @Override
    public void xuLyNapTien(ATM atm, int soTien) {
        atm.soTien = atm.soTien + soTien;
        System.out.println("Nạp thành công! số dư hiện tại: " + atm.soTien);
    }

    @Override
    public void xuLyRutTien(ATM atm) {
        Scanner scanner = new Scanner(System.in);
        System.out.println("Mời bạn nhập số tiền rút 1: 3.000.000đ; 2: 5.000.000đ: >");
        int luaChon = scanner.nextInt();
        int soTien = atm.soTien;
        if (luaChon == 1){
            atm.soTien -= atm.soTien - 3_000_000;
            System.out.println("Mời bạn nhận tiền: 3.000.000đ");
        }else if (luaChon == 2){
            atm.soTien -= atm.soTien - 5_000_000;
            System.out.println("Mời bạn nhận tiền: 5.000.000đ");
        }
        if (soTien > atm.soTien){
            if (atm.soTien < 5_000_000 && atm.soTien > 0)
                atm.setState(new HanChe());
            else if (atm.soTien == 0)
                atm.setState(new HetTien());
        }
        else {
            System.out.println("Nhập sai số tiền!!!");
        }
    }
}
