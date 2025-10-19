package structural.composite;

public class MainCayNhiPhan {
    public static void main(String[] args) {
        Nut goc = new Nuttrong(10);
        Nut n1 = new Nuttrong(5);
        Nut n2 = new Nuttrong(15);
        Nut l1 = new NutLa(1);
        Nut l2 = new NutLa(6);
        Nut l3 = new NutLa(12);
        Nut l4 = new NutLa(17);
        goc.ThemTrai(n1);
        goc.ThemPhai(n2);
        n1.ThemTrai(l1);
        n1.ThemPhai(l2);
        n2.ThemTrai(l3);
        n2.ThemPhai(l4);
        goc.Duyet();
        System.out.println("\nNhánh 1:");
        n1.Duyet();
        System.out.println("\nNhánh 2:");
        n2.Duyet();
    }
}
