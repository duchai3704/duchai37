package structural.decorator.beverage;

public class Espresso extends Beverage{
    public Espresso(String description) {
        super(description);
    }

    @Override
    public int cost() {
        return 25000;
    }

}
