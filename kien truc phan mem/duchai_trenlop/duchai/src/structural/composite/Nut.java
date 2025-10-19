package structural.composite;

public abstract class Nut {
    int giaTri;

    public Nut(int giaTri) {
        this.giaTri = giaTri;
    }

    public abstract void ThemTrai(Nut var1);

    public abstract void ThemPhai(Nut var1);

    public abstract void XoaTrai();

    public abstract void XoaPhai();

    public abstract void Duyet();
}
