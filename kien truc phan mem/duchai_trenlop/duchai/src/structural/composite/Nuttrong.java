package structural.composite;

public class Nuttrong extends Nut{
    Nut trai;
    Nut phai;

    public Nuttrong(int giaTri) {
        super(giaTri);
    }

    public void ThemTrai(Nut nut) {
        this.trai = nut;
    }

    public void ThemPhai(Nut nut) {
        this.phai = nut;
    }

    public void XoaTrai() {
        this.trai = null;
    }

    public void XoaPhai() {
        this.phai = null;
    }

    public void Duyet() {
        if (this.trai != null) {
            this.trai.Duyet();
        }

        System.out.print(" " + this.giaTri);
        if (this.phai != null) {
            this.phai.Duyet();
        }

    }
}
